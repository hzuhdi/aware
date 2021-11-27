# Stage 0, "build-stage", based on Node.js, to build and compile the frontend
FROM tiangolo/node-frontend:10 as build-stage
WORKDIR /app
COPY package*.json /app/
RUN npm install
ARG REACT_APP_AUTH0_DOMAIN
ENV REACT_APP_AUTH0_DOMAIN $REACT_APP_AUTH0_DOMAIN

ARG REACT_APP_AUTH0_CLIENT_ID
ENV REACT_APP_AUTH0_CLIENT_ID $REACT_APP_AUTH0_CLIENT_ID

ARG REACT_APP_BACKEND_URL
ENV REACT_APP_BACKEND_URL $REACT_APP_BACKEND_URL

COPY ./ /app/
RUN npm run build
# Stage 1, based on Nginx, to have only the compiled app, ready for production with Nginx
FROM nginx:1.15
COPY --from=build-stage /app/build/ /usr/share/nginx/html
# Copy the default nginx.conf provided by tiangolo/node-frontend
COPY --from=build-stage /nginx.conf /etc/nginx/conf.d/default.conf