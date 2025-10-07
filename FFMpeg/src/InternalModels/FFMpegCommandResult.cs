using System;
using CliWrap.Buffered;

namespace StableCube.Media.FFMpeg
{
    public class FFMpegCommandResult : BufferedCommandResult
    {
        public FFMpegCommandResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime, string standardOutput, string standardError)
            : base(exitCode, startTime, exitTime, standardOutput, standardError) {}

        public FFMpegCommandResult(BufferedCommandResult source)
            : base(source.ExitCode, source.StartTime, source.ExitTime, source.StandardOutput, source.StandardError) {}
    }
}