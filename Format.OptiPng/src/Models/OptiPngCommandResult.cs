using System;
using CliWrap.Buffered;

namespace StableCube.Media.OptiPng
{
    public class OptiPngCommandResult : BufferedCommandResult
    {
        public OptiPngCommandResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime, string standardOutput, string standardError)
            : base(exitCode, startTime, exitTime, standardOutput, standardError) {}

        public OptiPngCommandResult(BufferedCommandResult source)
            : base(source.ExitCode, source.StartTime, source.ExitTime, source.StandardOutput, source.StandardError) {}
    }
}
