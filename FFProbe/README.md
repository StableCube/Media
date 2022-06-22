## What is this?

Wrapper for building and running FFProbe commands

## Installation
Nuget package available [Here](https://www.nuget.org/packages/StableCube.Media.FFProbe/)

FFProbe must also be installed on the system.

## Example Commands

Probe file and get the format from the results
```
FFProbeService probe = new FFProbeService();
var fileInfo = await probe.FileInfoAsync("/path/to/file.mp4");

string format = fileInfo.Format.FormatName;
```

Count the frames in a video by first trying a quick technique that then falls back to a slower but the more thorough technique if not successful
```
FFProbeService probe = new FFProbeService();
int result = await probe.FrameCountSmartAsync("/path/to/file.mp4", 0);
```