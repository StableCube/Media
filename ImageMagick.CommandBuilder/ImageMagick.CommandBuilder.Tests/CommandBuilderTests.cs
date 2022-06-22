using System;
using Xunit;
using StableCube.Media.ImageMagick;

namespace StableCube.Media.ImageMagick.CommandBuilder.Tests
{
    public class CommandBuilderTests
    {
        public CommandBuilderTests()
        {

        }

        [Fact]
        public void Should_Build_Command_To_Overlay_Watermark()
        {
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

            Assert.Contains("-compose multiply", cmd.ToString());
        }
    }
}