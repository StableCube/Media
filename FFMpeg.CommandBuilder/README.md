## What is this?
A serializable tool to build FFMpeg command line commands

## Installation
Nuget package available [Here](https://www.nuget.org/packages/StableCube.Media.FFMpeg.CommandBuilder/)

## Usage Example

Build a basic command that outputs H264 with medium preset.
```
CommandTaskBuilder cmdBuilder = new CommandTaskBuilder();
CommandOptionGroupBuilder optGroupBuilder = cmdBuilder
    .BuildOptionGroup(0)
    .AddInput(0, "/path/to/input.mp4")
    .AddOutput(0, "/path/to/output.mp4");

optGroupBuilder.GlobalScope.GenericOptions
    .AddLogLevel(Options.Generic.FFMpegLogLevel.Error)
    .AddHideBanner();

optGroupBuilder.OutputScope
.Libx264Options.AddPreset(Options.Libx264.Preset.Medium)
.AudioOptions.AddAudioChannels(2);

# get the command to pass to FFMpeg
string command = optGroupBuilder.CommandOptionGroup.ToString();
```

Results in
```
-loglevel 16 -hide_banner -i /path/to/input.mp4 -preset medium -ac 2 /path/to/output.mp4
```