import json
import random

class VideoAnalysisReport:
    def __init__(self, filename, deepfakePercentage):
      self.filename = filename
      self.deepfakePercentage = deepfakePercentage
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
            sort_keys=True, indent=4)
                
def analyzeVideo(filename):
    return VideoAnalysisReport(filename, random.uniform(0, 100)).toJSON()


