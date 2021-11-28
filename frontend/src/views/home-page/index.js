// material-ui
import { Typography } from '@mui/material';
import { Link } from "react-router-dom";
import { useAuth0 } from '@auth0/auth0-react';

// project imports
import MainCard from 'ui-component/cards/MainCard';
import Button from '@mui/material/Button';

// ==============================|| SAMPLE PAGE ||============================== //

const HomePage = () => {
    const { isAuthenticated } = useAuth0();

    return (
        <MainCard title="Welcome to Aware Platform! 👀" align="center">
            <Typography variant="body1" align="center">
                Aware is DeepFake detection solution 🌎 built on top of different existing pre-trained open source models
            </Typography>
            <br></br>
            <br></br>
            <Typography align="center">
                {
                    isAuthenticated === false && 
                <Link to="/login" style={{ textDecoration: 'none' }}>
                    <Button variant="contained">Login / SignUp</Button>
                </Link>
                }
            </Typography>

        </MainCard>
    );
};

export default HomePage;
