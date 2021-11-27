import json
import os
import sys
import time

import cv2
import numpy as np
import pandas as pd
import timm
import torch
import torch.nn as nn
import torchvision.transforms as transforms
from albumentations import Resize
from facedetector.retinaface import df_retinaface
from tqdm import tqdm


def detect_single(video_path):
    """Perform deepfake detection on a single video with a chosen method."""
    # prepare the method of choice
    method = "efficientnetb7_dftimit_lq"
    # method1 = efficientnetb7_dftimit_lq
    # method2 = efficientnetb7_dfdc

    model, img_size, normalization = prepare_method(method=method)

    dataPred = []

    if video_path:
        data = [[1, video_path]]
        df = pd.DataFrame(data, columns=['label', 'video'])
        loss = inference(model, df, img_size, normalization, method=method, face_margin=0.3,
                                       num_frames=20, single=True)

        p_loss = loss[0] * 100
        percent = str(p_loss) + "%"
        if round(np.round(loss)[0]) == 1:
            result = "Deepfake detected."
            print("Deepfake detected.")
            dataPred.append({"filename": video_path, "label": result, "percentage": percent})
            return json.dumps(dataPred)
        else:
            result = "This is a real video."
            print("This is a real video.")
            dataPred.append({"filename": video_path, "label": result, "percentage": percent})
            return json.dumps(dataPred)


def prepare_method(method):
    """Prepares the method that will be used for training or benchmarking."""
    # 380 image size as introduced here https://www.kaggle.com/c/deepfake-detection-challenge/discussion/145721
    img_size = 380
    normalization = 'imagenet'

    # successfully used by https://www.kaggle.com/c/deepfake-detection-challenge/discussion/145721 (noisy student weights)
    model = timm.create_model('tf_efficientnet_b7_ns', pretrained=True)
    model.classifier = nn.Linear(2560, 1)
    # load the efficientnet model that was pretrained on the uadfv training data
    model_params = torch.load(os.getcwd() + f'/video/models/{method}.pth')
    model.load_state_dict(model_params)
    return model, img_size, normalization


def inference(model, test_df, img_size, normalization, method, face_margin,
              num_frames=None, single=False):
    running_loss = 0.0
    running_corrects = 0.0
    running_false = 0.0
    running_auc = []
    running_ap = []
    labs = []
    prds = []
    ids = []
    frame_level_prds = []
    frame_level_labs = []
    running_corrects_frame_level = 0.0
    running_false_frame_level = 0.0
    # load retinaface face detector
    net, cfg = df_retinaface.load_face_detector()
    inference_time = time.time()
    print(f"Inference using {num_frames} frames per video.")
    print(f"Use face margin of {face_margin * 100} %")
    for idx, row in tqdm(test_df.iterrows(), total=test_df.shape[0]):
        video = row.loc['video']
        label = row.loc['label']
        vid = os.path.join(video)
        # inference (no saving of images inbetween to make it faster)
        # detect faces, add margin, crop, upsample to same size, save to images
        faces = df_retinaface.detect_faces(net, vid, cfg, num_frames=num_frames)
        # save frames to images
        # try:
        vid_frames = df_retinaface.extract_frames(
            faces, video, save_to=None, face_margin=face_margin, num_frames=num_frames, test=True)
        if single:
            name = video[:-4] + ".jpg"
        # if no face detected continue to next video
        if not vid_frames:
            print("No face detected.")
            continue
        # inference for each frame
        # not sequence model:
        # frame level auc can be measured
        vid_pred, vid_loss, frame_level_preds = vid_inference(
            model, vid_frames, label, img_size, normalization)
        frame_level_prds.extend(frame_level_preds)
        frame_level_labs.extend([label] * len(frame_level_preds))
        running_corrects_frame_level += np.sum(
            np.round(frame_level_preds) == np.array([label] * len(frame_level_preds)))
        running_false_frame_level += np.sum(
            np.round(frame_level_preds) != np.array([label] * len(frame_level_preds)))

        ids.append(video)
        labs.append(label)
        prds.append(vid_pred)
        running_loss += vid_loss
        # calc accuracy; thresh 0.5
        running_corrects += np.sum(np.round(vid_pred) == label)
        running_false += np.sum(np.round(vid_pred) != label)

    # save predictions to csv for ensembling
    df = pd.DataFrame(list(zip(ids, labs, prds)), columns=[
        'Video', 'Label', 'Prediction'])
    print(prds)
    # prd = np.round(prds)
    return prds # prd[0]

def vid_inference(model, video_frames, label, img_size, normalization):
    # model evaluation mode
    model.cuda()
    model.eval()
    device = "cuda" if torch.cuda.is_available() else "cpu"
    loss_func = nn.BCEWithLogitsLoss()
    # label = torch.from_numpy(label).to(device)
    # get prediction for each frame from vid
    avg_vid_loss = []
    avg_preds = []
    avg_loss = []
    frame_level_preds = []

    for frame in video_frames:
        # turn image to rgb color
        frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        # resize to DNN input size
        resize = Resize(width=img_size, height=img_size)
        frame = resize(image=frame)['image']
        frame = torch.tensor(frame).to(device)
        # forward pass of inputs and turn on gradient computation during train
        with torch.no_grad():
            # predict for frame
            # channels first
            frame = frame.permute(2, 0, 1)
            # turn dtype from uint8 to float and normalize to [0,1] range
            frame = frame.float() / 255.0
            # normalize by imagenet stats
            if normalization == "imagenet":
                transform = transforms.Normalize(
                    [0.485, 0.456, 0.406], [0.229, 0.224, 0.225])
            frame = transform(frame)
            # add batch dimension and input into model to get logits
            predictions = model(frame.unsqueeze(0))
            # get probabilitiy for frame from logits
            preds = torch.sigmoid(predictions)
            avg_preds.append(preds.cpu().numpy())
            frame_level_preds.extend(preds.cpu().numpy()[-1])
            # calculate loss from logits
            loss = loss_func(predictions.squeeze(1), torch.tensor(
                label).unsqueeze(0).type_as(predictions))
            avg_loss.append(loss.cpu().numpy())
    # return the prediction for the video as average of the predictions over all frames
    return np.mean(avg_preds), np.mean(avg_loss), frame_level_preds

def main():
    if len(sys.argv) != 2:
        print('usage: detect.py <videos_path>')
        exit(1)

    print(detect_single(sys.argv[1]))

if __name__ == '__main__':
	np.warnings.filterwarnings('ignore', category=np.VisibleDeprecationWarning)
	main()