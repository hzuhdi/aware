// project imports
import AuthWrapper1 from '../AuthWrapper1';
import { useAuth0 } from '@auth0/auth0-react';
import React from 'react';

// ===============================|| AUTH3 - REGISTER ||=============================== //

const Register = () => {
    const { loginWithRedirect, isAuthenticated } = useAuth0();
    React.useEffect(() => {
        if (isAuthenticated === false) {
            loginWithRedirect({
                screen_hint: 'signup',
            });
        }
        
    }, [])

    return (
        <AuthWrapper1>
        </AuthWrapper1>
    );
};

export default Register;
