using System;
using CliWrap.Buffered;

namespace StableCube.Media.WebP
{
    public class WebPCommandResult : BufferedCommandResult
    {
        public WebPCommandResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime, string standardOutput, string standardError)
            : base(exitCode, startTime, exitTime, standardOutput, standardError) {}

        public WebPCommandResult(BufferedCommandResult source)
            : base(source.ExitCode, source.StartTime, source.ExitTime, source.StandardOutput, source.StandardError) {}
    }
}
