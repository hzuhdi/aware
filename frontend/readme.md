## Aware Frontend

---

### How to run Aware Application ?

1. W/O docker 🤫

```
# Go to folder frontend
cd frontend
# Create env var file
touch .env
```

```
# create following lines
REACT_APP_AUTH0_DOMAIN = <Your AUTH0 domain>
REACT_APP_AUTH0_CLIENT_ID = <Your AUTH0 client ID>
REACT_APP_BACKEND_URL =  <Your Backend URL: see backend part>
```

```
# Install node modules
npm install

# Spin up the development server
npm run start
```

2. With docker 🐳

```
docker build -t <yourimagename> . 
--build-arg REACT_APP_AUTH0_DOMAIN=<domain> \
--build-arg REACT_APP_AUTH0_CLIENT_ID=<clientID> \
--build-arg REACT_APP_BACKEND_URL=<URL> \
```

```
docker run -it --rm -p 80:80 <imagename>:latest   
```

