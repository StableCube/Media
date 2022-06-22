using System;
using Xunit;

namespace StableCube.Media.FFMpeg.CommandBuilder.Tests
{
    public class CommandBuilderTests
    {
        public CommandBuilderTests()
        {

        }

        [Fact]
        public void Should_Build_Command()
        {
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

            Assert.Contains("-loglevel", optGroupBuilder.CommandOptionGroup.ToString());
        }
    }
}