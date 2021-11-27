import React, { useState } from 'react';
import { styled } from '@mui/material/styles';
import { Grid } from '@mui/material';
import MuiTypography from '@mui/material/Typography';

// project imports
import SubCard from 'ui-component/cards/SubCard';
import MainCard from 'ui-component/cards/MainCard';
import SecondaryAction from 'ui-component/cards/CardSecondaryAction';
import { gridSpacing } from 'store/constant';
import Button from '@mui/material/Button';
import axios from 'axios';

// ==============================|| Video ||============================== //

const Video = () => {
    const [file, setFile] = useState(null);
    const [fileName, setFileName] = useState('');
    const [fileNameResult, setFileNameResult] = useState('');
    const [detectionResult, setDetectionResult] = useState('');
    const [percentage, setPercentage] = useState('');

    const Input = styled('input')({
        display: 'none',
      });

    const handleChange = (event) => {
        setFile(event.target.files[0]);
        setFileName(event.target.files[0].name);
    };

    const uploadFile = () => {
        let formFile = new FormData();
        formFile.append('formFile', file);
        axios.post(`https://localhost:7057/Video/Scan`, formFile, {
            headers: {
                'Content-Type': 'multipart/form-data'
              }
          })
            .then(res => {
                console.log(res);
                if (res.status == 200) {
                    console.log(res.data);
                    setFileNameResult(res.data.filename ?? '');
                    setDetectionResult(res.data.label ?? '');
                    setPercentage(res.data.percentage ?? '');
                }
            })
    }

    return (
        <MainCard title="Video" secondary={<SecondaryAction link="https://next.material-ui.com/system/typography/" />}>
            <Grid container spacing={gridSpacing}>
                <Grid item xs={12} sm={6}>
                    <SubCard title="Upload">
                        <Grid container spacing={gridSpacing} style={{textAlign: "center"}} direction="column">
                            <Grid item>
                                <label htmlFor="contained-button-file">
                                    <Input accept="video/mp4,video/mov" id="contained-button-file" multiple type="file" onChange={handleChange} />
                                    <Button variant="contained" component="span">
                                        Browse Video
                                    </Button>
                                </label>
                            </Grid>
                            <Grid item>
                                <MuiTypography variant="h3" >
                                    {fileName}
                                </MuiTypography>
                            </Grid>
                            { file !== null && (<Grid item>
                                <Button variant="contained" component="span" onClick={uploadFile}>
                                    Upload
                                </Button>
                            </Grid>)}
                        </Grid>
                    </SubCard>
                </Grid>
                <Grid item xs={12} sm={6}>
                    <SubCard title="Result">
                        <Grid container spacing={gridSpacing}>
                            <Grid item xs={4}>
                                <MuiTypography variant="h4" >
                                    Filename
                                </MuiTypography>
                            </Grid>
                            <Grid item>
                                <MuiTypography variant="h4" >
                                    { fileNameResult !== '' ? fileNameResult : '-' }
                                </MuiTypography>
                            </Grid>
                        </Grid>
                        <Grid container spacing={gridSpacing}>
                            <Grid item xs={4}>
                                <MuiTypography variant="h4" >
                                    Detection Result
                                </MuiTypography>
                            </Grid>
                            <Grid item>
                                <MuiTypography variant="h4" >
                                    { detectionResult !== '' ? detectionResult : '-' }
                                </MuiTypography>
                            </Grid>
                        </Grid>
                        <Grid container spacing={gridSpacing}>
                            <Grid item xs={4}>
                                <MuiTypography variant="h4" >
                                    Percentage
                                </MuiTypography>
                            </Grid>
                            <Grid item>
                                <MuiTypography variant="h4" >
                                    { percentage !== '' ? percentage : '-' }
                                </MuiTypography>
                            </Grid>
                        </Grid>
                    </SubCard>
                </Grid>
            </Grid>
        </MainCard>
    );
};

export default Video;
