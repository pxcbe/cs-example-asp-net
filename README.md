# cs-example-asp-net
example on how to create aNET application that communicates with the PLCnext trough the REST API

# Building the Docker container

```
sudo docker build -t aspnetapp .
```

# Running the Docker container

```
sudo docker run -it -p 5000:80 myapp
```

# Installing balena on PLCnext

https://github.com/PLCnext/Docker_GettingStarted

## Running container on PLCnext

```
  as root
  
  balena-engine run run -it -p 5000:80 myapp
```
