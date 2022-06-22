using System;
using CliWrap.Buffered;
using StableCube.Media.ImageMagick.DataModel;

namespace StableCube.Media.ImageMagick
{
    public class ImageMagickProbeResult : BufferedCommandResult
    {
        public ImageInfo ImageInfo { get; private set; }

        public ImageMagickProbeResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime, string standardOutput, string standardError)
            : base(exitCode, startTime, exitTime, standardOutput, standardError) {}

        public ImageMagickProbeResult(BufferedCommandResult source, ImageInfo imgInfo)
            : base(source.ExitCode, source.StartTime, source.ExitTime, source.StandardOutput, source.StandardError)
        {
            ImageInfo = imgInfo;
        }
    }
}
