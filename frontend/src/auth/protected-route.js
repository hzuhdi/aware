// src/auth/protected-route.js

import React from "react";
import { Route } from "react-router-dom";
import { withAuthenticationRequired } from "@auth0/auth0-react";
import { SamplePage } from "../views/sample-page";

const ProtectedRoute = ({ component, ...args }) => (
  <Route
    component={withAuthenticationRequired(component, {
      onRedirecting: () => <SamplePage />,
    })}
    {...args}
  />
);

export default ProtectedRoute;
