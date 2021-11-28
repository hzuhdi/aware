import React from 'react';

// material-ui
import { useAuth0 } from '@auth0/auth0-react';

// project imports
import AuthWrapper1 from '../AuthWrapper1';

// ================================|| AUTH3 - LOGIN ||================================ //

const Login = () => {
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
