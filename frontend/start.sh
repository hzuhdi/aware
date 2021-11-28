#!/bin/sh

envsubst '${BACKEND_SERVICE}' < "/etc/nginx/conf_with_env" > "/etc/nginx/nginx.conf"
nginx -g "daemon off;"