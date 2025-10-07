using System;
using CliWrap.Buffered;

namespace StableCube.Media.JpegOptim
{
    public class JpegOptimCommandResult : BufferedCommandResult
    {
        public JpegOptimCommandResult(int exitCode, DateTimeOffset startTime, DateTimeOffset exitTime, string standardOutput, string standardError)
            : base(exitCode, startTime, exitTime, standardOutput, standardError) {}

        public JpegOptimCommandResult(BufferedCommandResult source)
            : base(source.ExitCode, source.StartTime, source.ExitTime, source.StandardOutput, source.StandardError) {}
    }
}
