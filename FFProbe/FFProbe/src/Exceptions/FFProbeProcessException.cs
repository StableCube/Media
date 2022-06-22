using System;

namespace StableCube.Media.FFProbe
{
    public class FFProbeProcessException : Exception
    {
        public FFProbeProcessException(string message): base(message)
        {
        }

        public FFProbeProcessException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
