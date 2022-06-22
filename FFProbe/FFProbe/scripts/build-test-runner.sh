#!/bin/sh

DOCKER_IMAGE=ffprobe-testrunner
DOCKER_BUILD_TAG=2.2-2

set -e

docker build --file "$(pwd)/testrunner.Dockerfile" -t us.gcr.io/stablecube/$DOCKER_IMAGE:$DOCKER_BUILD_TAG .
docker push us.gcr.io/stablecube/$DOCKER_IMAGE:$DOCKER_BUILD_TAG