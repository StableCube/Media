using System;

namespace StableCube.Media.FFMpeg
{
    public class FFMpegProcessException : Exception
    {
        public FFMpegProcessException(string message): base(message)
        {
        }

        public FFMpegProcessException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
