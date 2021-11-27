// material-ui
import { Typography } from '@mui/material';

// project imports
import MainCard from 'ui-component/cards/MainCard';
import Button from '@mui/material/Button';

// ==============================|| SAMPLE PAGE ||============================== //

const SamplePage = () => (
    <MainCard title="Welcome to Aware Platform! 👀" align="center">
        <Typography variant="body1" align="center">
            Aware is DeepFake detection solution 🌎 built on top of different existing pre-trained open source models
        </Typography>
        <br></br>
        <br></br>
        <Typography align="center">
            <Button variant="contained">Login / SignUp</Button>
        </Typography>

    </MainCard>
);

export default SamplePage;
