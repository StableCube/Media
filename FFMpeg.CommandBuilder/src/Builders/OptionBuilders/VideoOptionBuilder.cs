using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Video;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class VideoOptionBuilder : OptionBuilder
    {
        public VideoOptionBuilder(List<IOption> source) : base(source)
        {
        }

        public VideoOptionBuilder AddAspectRatio(string aspectRatio, StreamSpecifier stream = null)
        {
            Options.Add(new AspectRatioOption(stream)
            {
                AspectRatio = aspectRatio
            });

            return this;
        }

        public VideoOptionBuilder AddFramerate(int frameRate, StreamSpecifier stream = null)
        {
            Options.Add(new FramerateOption(stream)
            {
                FrameRate = frameRate
            });

            return this;
        }

        public VideoOptionBuilder AddFrameSize(int width, int height, StreamSpecifier stream = null)
        {
            Options.Add(new FrameSizeOption(stream)
            {
                Width = width,
                Height = height
            });

            return this;
        }

        public VideoOptionBuilder AddPass(int passNumber, StreamSpecifier stream = null)
        {
            Options.Add(new PassOption(stream)
            {
                PassNumber = passNumber
            });

            return this;
        }
    }
}