## What is this?

Wrapper for running Image Magick commands

## Installation
Nuget package available [Here](https://www.nuget.org/packages/StableCube.Media.ImageMagick/)

## Example Usage

Probe image file for image info
```
ImageInfo result = await MagickCommand.ProbeAsync(sourceFilePath);
```

Convert png to jpg
```
var cmd = new ImageMagickCommand();
cmd.AddInputFile(new InputFile("/path/to/input.png"));
cmd.OutputFilePath = "/path/to/output.jpg";

var converter = new MagickConvert();
ImageMagickCommandResult result = await converter.RunCommandAsync(cmd);
```