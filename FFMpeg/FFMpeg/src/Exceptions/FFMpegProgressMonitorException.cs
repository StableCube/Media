using System;

namespace StableCube.Media.FFMpeg
{
    public class FFMpegProgressMonitorException : Exception
    {
        public FFMpegProgressMonitorException()
        {
        }

        public FFMpegProgressMonitorException(string message): base(message)
        {
        }

        public FFMpegProgressMonitorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
