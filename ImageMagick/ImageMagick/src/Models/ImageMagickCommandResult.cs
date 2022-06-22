using System;
using CliWrap.Buffered;
using StableCube.Media.ImageMagick.DataModel;

namespace StableCube.Media.ImageMagick
{
    public class ImageMagickCommandResult : BufferedCommandResult
    {
        public ImageMagickCommandResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime, string standardOutput, string standardError)
            : base(exitCode, startTime, exitTime, standardOutput, standardError) {}

        public ImageMagickCommandResult(BufferedCommandResult source)
            : base(source.ExitCode, source.StartTime, source.ExitTime, source.StandardOutput, source.StandardError) {}
    }
}
