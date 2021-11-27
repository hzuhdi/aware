import React, { useState } from 'react';
import { Grid, Link } from '@mui/material';
import MuiTypography from '@mui/material/Typography';

// project imports
import SubCard from 'ui-component/cards/SubCard';
import MainCard from 'ui-component/cards/MainCard';
import SecondaryAction from 'ui-component/cards/CardSecondaryAction';
import { gridSpacing } from 'store/constant';
import { DropzoneArea } from 'material-ui-dropzone'
import AttachFile from '@material-ui/icons/AttachFile';
import Button from '@material-ui/core/Button';
import VideoUploadFile from './VideoUploadFile';
import axios from 'axios';

// ==============================|| Video ||============================== //

const Video = () => {
    const [files, setFiles] = useState([]);

    const handleChange = (files) => {
        setFiles(files);
    };

    const uploadFile = () => {
        console.log(files);
        let fd = new FormData();
        files.map((file) => {
            fd.append('File[]', file);
        });

        axios.post(`https://localhost:7057/Video/Scan`, fd, {
            headers: {
                'Content-Type': 'multipart/form-data'
              }
          })
            .then(res => {
                console.log(res);
                console.log(res.data);
            })
    }

    return (
        <MainCard title="Video" secondary={<SecondaryAction link="https://next.material-ui.com/system/typography/" />}>
            <Grid container spacing={gridSpacing}>
                <Grid item xs={12} sm={12}>
                    <DropzoneArea
                        onChange={handleChange.bind(this)}
                        dropzoneText='Drag and drop a file here or click (mp4 or mov)'
                        acceptedFiles={['video/mp4','video/mov']}
                        maxFileSize={25000000} // 25mb
                        filesLimit={1} />
                </Grid>
            </Grid>
            <Grid container spacing={gridSpacing}>
                <Grid item xs={12} sm={12}>
                    <Button variant="outlined" color="primary" onClick={uploadFile.bind(this)}>
                        Upload
                    </Button>
                </Grid>
            </Grid>
        </MainCard>
    );
};

export default Video;
