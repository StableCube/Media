## What is this?
Wrapper for running/building FFMpeg commands with the ability to track progress and parse log files.

## Installation
Nuget package available [Here](https://www.nuget.org/packages/StableCube.Media.FFMpeg/)

FFMpeg must also be installed on the system.

## Usage Examples

Transcode a video with default settings with progress tracking
```
var ffmpeg = new FFMpegService();
var cmd = new FFMpegCommandTask();
var optionGroup = new FFMpegCommandOptionGroup(0);
optionGroup.AddInputFile("/path/to/source.mp4");
optionGroup.AddOutputFile("/path/to/output.mp4");

cmd.CommandOptionGroups = new SortedDictionary<int, FFMpegCommandOptionGroup>()
{
    { 0, optionGroup },
};

var progress = new Progress<ProgressResult>((progressResult) => {
    Console.WriteLine($"Current Progress {progressResult.Progress}");
});

FFMpegLogOptions logOpts = new FFMpegLogOptions()
{
    LogFilePath = "/path/to/log_file.log",
    SourceFilePath = "/path/to/source.mp4",
    ProgressData = progress
};

var cmdResult = await ffmpeg.RunCommandTaskAsync(cmd, logOpts);
```