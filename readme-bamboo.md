# Bamboo Builds With .Net Core

## Sample Build

This is my POC build that builds .net core code and publishes it to a Docker image: [http://bamboo.cdk.com/browse/QUOTE-TRPOC](http://bamboo.cdk.com/browse/QUOTE-TRPOC)

Built images can be found in [Artifactory here](http://artifactory.cobalt.com/artifactory/webapp/#/artifacts/browse/simple/search/package/eyJxdWVyeSI6W3siaWQiOiJkb2NrZXJWMkltYWdlIiwidmFsdWVzIjpbInF1b3RlLWNsb3VkLXBvYyJdfSx7ImlkIjoicmVwbyIsInZhbHVlcyI6W119XSwic2VsZWN0ZWRQYWNrYWdlVHlwZSI6eyJpZCI6ImRvY2tlclYyIiwiaWNvbiI6ImRvY2tlciIsImRpc3BsYXlOYW1lIjoiRG9ja2VyIn0sInNlbGVjdGVkUmVwb3NpdG9yaWVzIjpbXSwiY29sdW1ucyI6WyJkb2NrZXJWMkltYWdlKkltYWdlQCIsImRvY2tlclYyVGFnKlRhZ0AiLCJyZXBvIiwibW9kaWZpZWQiXX0=)


# How To Guide

## Build .Net Core

Create a new build in bamboo.  Set the job details to whatever you like.

### Variables

Before getting started add two variables to the build.

`appname` with a value of `quote-cloud-poc`
`version` with a value of `0.1` (or whatever you want your app version to be)

These variables will be used later in the Docker build process.

### Tasks

#### Source Code Checkout

Enter the details for your repo.

#### MSBuild

##### Executable
MSBuild v15.0 (32bit), 64-bit has an error in the current version and will fail.

##### Project File
I used the .csproj file for my project.  I didn't have a solution file since this was a simple project I created.

##### Options
`/t:restore /t:Publish /p:OutputPath=release-api /p:Configuration=Release`

This will restore nuget dependencies, publish in release mode, to the release-api / release-apipublish directory (see below).

Has the same result as running this: `dotnet publish --output release-api --configuration release`

##### Working sub directory: 
src


### Artifacts

Make sure to share your artifacts if you plan to use them in another stage or build (if you're following along with this you will).

#### Artifact 1

- Name: `Dockerfile`
- Location: `src`
- Copy Pattern: `Dockerfile`
    - Note case matters as later Docker build will run on a *nix agent

#### Artifact 2

- Name: `release-api`
- Location: `src/release-apipublish`
- Copy Pattern: `*.*`

    _Note: The output path on the options was set to `release-api`, locally this would include all the built files.  On Bamboo with MSBuild puts all of the binaries into `release-apipublish`.  Be aware of this if you want all the files needed to run your API rather than just the files that build from your code._

## Build Docker Image

Building the Docker image needs to be a separate stage.  MSBuild will run on a Windows agent, Docker Build runs on a *nix agent.

### Tasks

#### Artifact Download 

I downloaded the two artifacts separately.  The Dockerfile needs to be in the relative path `/src`.  The release-api artifact should be in `/src/release-api` as that was what was specified in the Dockerfile.

#### Label Build Command

Add a new command

##### Executable
Label Build

##### Argument
${bamboo.appname}-${bamboo.version}.${bamboo.buildNumber}

#### Build Docker Image Command

Add a new command

_Note: I found [this document](https://confluence.cdk.com/display/ALM/Cloud+Platform%3A+How+to+configure+a+Bamboo+Build+Plan+to+build+a+Docker+Image) that provided some guidance with the Docker Build task_

##### Executable
Docker Build

##### Argument
-t ${bamboo.appname}

##### Working sub directory
src

## Artifactory

The Docker Build task will automatically push the built Docker image to a registry on Artifactory.

I was able to find the image(s) from my build process [here](http://artifactory.cobalt.com/artifactory/webapp/#/artifacts/browse/simple/search/package/).  Filter to view "Docker" and give it an Image name of what was set as your appname variable at the start of this document.

## Deployments With Bamboo

Before setting up a deployment, make sure you have a successful build.  The build should be labeled as noted above.

I set up a deployment based on this Cloud Platform document - [Cloud Platform: How to configure a Bamboo Deployment Project to deploy a Docker Container using CoreOS(V2)](https://confluence.cdk.com/pages/viewpage.action?pageId=127228461)

It is important to make sure you have set up the release versioning properly - [Cloud Platform: Configure the Release Versioning in a CoreOS deployment project (Release Versioning)](https://confluence.cdk.com/pages/viewpage.action?pageId=114113921)

Your API will need to have an endpoint of '/health' as part of the deployment will be to check it for status to make sure the service started properly.

One issue I encountered was related to the Consul service name.  Per the Cloud Platform team, these are the current Consul ACL Tokens:
```
service "dm-" {
    policy = "write"
}
service "ds-" {
    policy = "write"
}
service "dms-" {
    policy = "write"
}
service "alm-" {
    policy = "write"
}
service "ei-" {
    policy = "write"
}
key "service/compute" {
  policy = "write"
}
service "nc-" {
    policy = "write"
}
service "compute" {
    policy = "write"
}
service "cs-" {
    policy = "write"
}
service "ari-" {
    policy = "write"
}
```

### Starting up multiple instances

It is possible to start up multiple instances of the service during the deployment step "CDK Cloud Activation".  The number of hosts varies depending on the environment the service runs in, but for the most part we can have 1-6 instances running across separate hosts for high availability.

### Sample Deployment Plan

The deployment plan I created as a part of the POC is [here](http://bamboo.cdk.com/deploy/viewDeploymentProjectEnvironments.action?id=397738028).

The running API can be seen here:

*Sample Health Endpoint*
- [http://api-int.dit.connectcdk.com/api/dm-quote-cloud-poc/v1/health](http://api-int.dit.connectcdk.com/api/dm-quote-cloud-poc/v1/health)

*Sample GET Endpoint*
- [http://api-int.dit.connectcdk.com/api/dm-quote-cloud-poc/v1/api/opportunity/foo](http://api-int.dit.connectcdk.com/api/dm-quote-cloud-poc/v1/api/opportunity/foo)