using System;
using CliWrap.Buffered;

namespace StableCube.Media.Gifsicle
{
    public class GifsicleCommandResult : BufferedCommandResult
    {
        public GifsicleCommandResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime, string standardOutput, string standardError)
            : base(exitCode, startTime, exitTime, standardOutput, standardError) {}

        public GifsicleCommandResult(BufferedCommandResult source)
            : base(source.ExitCode, source.StartTime, source.ExitTime, source.StandardOutput, source.StandardError) {}
    }
}
