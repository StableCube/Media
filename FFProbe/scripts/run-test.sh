#!/bin/sh

# pass filters like this: 
# --filter "FullyQualifiedName=StableCube.Media.Tools.ImageMagick.Tests.ConvertTests.Should_Identify_Animated_Gif"

set -e

docker run \
    -v $(pwd)/../../:/app \
    -v /home/zboyet/Documents/TestMedia:/TestMedia \
    -w="/app/Tools/FFProbe" \
    --rm \
    -it \
    us.gcr.io/stablecube/ffprobe-testrunner:2.2-2 \
    -i $@
