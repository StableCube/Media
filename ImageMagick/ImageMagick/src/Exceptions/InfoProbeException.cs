using System;

namespace StableCube.Media.ImageMagick
{
    public class InfoProbeException : Exception
    {
        public InfoProbeException(string message): base(message)
        {
        }

        public InfoProbeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
