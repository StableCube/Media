## What is this?

A serializable tool to build Image Magick command line commands

## Example Usage

Create a command that will overlay one image on top of another in a watermark fashion
```
var imageInput = new InputFile("/path/to/input.jpg");
var watermarkInput = new InputFile("/path/to/watermark.jpg");

watermarkInput.OptionSequence.Add(new ComposeOption()
{
    Compose = new CompositionMethod(ComposeType.Multiply)
});

watermarkInput.OptionSequence.Add(new GravityOption()
{
    Gravity = GravityOption.GravityType.South
});

watermarkInput.OptionSequence.Add(new CompositeOption()
{
    Alternate = true
});

var cmd = new ImageMagickCommand("/path/to/output.jpg");
cmd.AddInputFile(imageInput, 0);
cmd.AddInputFile(watermarkInput, 1);

string result = cmd.ToString();
```

Results in the below command that can be ran by prepending the path to the Image Magick binary
```
/path/to/input.jpg /path/to/watermark.jpg -compose multiply -gravity South +composite /path/to/output.jpg
```