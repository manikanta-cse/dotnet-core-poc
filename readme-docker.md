# Docker-ize The Web Api

## Build & Run A Container

Built this on Win10 Docker version 17.03.1-ce-win12 (12058).

```bash
# verify the api works locally
dotnet restore
dotnet run

# publish the web api resources
dotnet publish --output release-api --configuration release

# (optional) run the published api
dotnet release-api\quote-cloud-poc.dll

# build to docker linux container
docker build -t quote-cloud-poc .

# run the container on 8080
docker run -it -p 8080:8080 quote-cloud-poc
```

## Install Docker on CentOS 7

Based on [https://docs.docker.com/engine/installation/linux/centos/](https://docs.docker.com/engine/installation/linux/centos/)

```bash
# update package database
sudo yum check-update

# install required packages
sudo yum install -y yum-utils device-mapper-persistent-data lvm2

# add docker stable repo (Docker CE)
sudo yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo

# update the yum package index
sudo yum makecache fast

# list docker cd packages
yum list docker-ce.x86_64  --showduplicates |sort -r

# install a specific version of docker
sudo yum install docker-ce-17.03.1.ce-1.el7.centos

# create this /etc/docker/daemon.json
sudo mkdir /etc/docker
sudo touch /etc/docker/daemon.json
sudo nano /etc/docker/daemon.json
```

Set the contents of /etc/docker/daemon.json to:
```
{
  "storage-driver": "devicemapper"
}
```

Save and exit nano.

```bash

# start docker
sudo systemctl start docker

# verify docker works by running hello-world image
sudo docker run hello-world

```

## Build & Run The Container on CentOS

**Need to replace some of this with a build process and pull down the image from somewhere and run it.**
We don't want to be building .net or docker on the server for prod.

```bash
# check the status of the service
sudo systemctl status kestrel-quote-cloud-poc.service

# make sure the service is stopped if it was active
sudo systemctl stop kestrel-quote-cloud-poc.service

# from your source directory pull latest
git pull

# publish the web api resources
dotnet publish --output release-api --configuration release

# build to docker linux container
sudo docker build -t quote-cloud-poc .

# run the container on 8080 detached (in background)
sudo docker run -d -p 8080:8080 quote-cloud-poc

# verify that you can make a request to the containerized web api
curl localhost:8080/api/opportunity/foo

# at this point you should see the JSON response object come back
```