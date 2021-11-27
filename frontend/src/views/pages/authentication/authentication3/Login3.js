import { Link } from 'react-router-dom';
import React from 'react';

// material-ui
import { useTheme } from '@mui/material/styles';
import { useAuth0 } from '@auth0/auth0-react';

// project imports
import AuthWrapper1 from '../AuthWrapper1';

// ================================|| AUTH3 - LOGIN ||================================ //

const Login = () => {
    const theme = useTheme();
    const { loginWithRedirect, isAuthenticated } = useAuth0();
    React.useState(() => {
        if (isAuthenticated === false) {
            loginWithRedirect();
        } 
    }, [])

    return (
        <AuthWrapper1>
        </AuthWrapper1>
    );
};

export default Login;
