using System;
using CliWrap.Buffered;

namespace StableCube.Media.Gif2WebP
{
    public class Gif2WebPCommandResult : BufferedCommandResult
    {
        public Gif2WebPCommandResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime, string standardOutput, string standardError)
            : base(exitCode, startTime, exitTime, standardOutput, standardError) {}

        public Gif2WebPCommandResult(BufferedCommandResult source)
            : base(source.ExitCode, source.StartTime, source.ExitTime, source.StandardOutput, source.StandardError) {}
    }
}
