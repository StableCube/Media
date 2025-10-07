using System;
using CliWrap.Buffered;

namespace StableCube.Media.PngQuant
{
    public class PngQuantCommandResult : BufferedCommandResult
    {
        public PngQuantCommandResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime, string standardOutput, string standardError)
            : base(exitCode, startTime, exitTime, standardOutput, standardError) {}

        public PngQuantCommandResult(BufferedCommandResult source)
            : base(source.ExitCode, source.StartTime, source.ExitTime, source.StandardOutput, source.StandardError) {}
    }
}
