#!/bin/bash

# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # 
# Description: Script to dockerize the application and push it to the docker registry
# Author: Swamy
# Date: 20-Aug-2022
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # 

TAG=latest
VERSION_TAG=$(git log -1 --pretty=format:%h)
echo "version tag: $VERSION_TAG"

REPOSITORY=$DOCKER_USERNAME/$DOCKER_IMAGE_IDENTITY

docker login

docker build -f "./Source/Identity.API/Dockerfile" -t $REPOSITORY:$TAG -t $REPOSITORY:$VERSION_TAG .

docker push $REPOSITORY:$TAG
docker push $REPOSITORY:$VERSION_TAG