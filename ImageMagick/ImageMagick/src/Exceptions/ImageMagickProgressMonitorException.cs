using System;

namespace StableCube.Media.ImageMagick
{
    public class ImageMagickProgressMonitorException : Exception
    {
        public ImageMagickProgressMonitorException()
        {
        }

        public ImageMagickProgressMonitorException(string message): base(message)
        {
        }

        public ImageMagickProgressMonitorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
