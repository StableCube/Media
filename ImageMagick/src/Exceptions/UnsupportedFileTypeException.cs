using System;

namespace StableCube.Media.ImageMagick
{
    public class UnsupportedFileTypeException : Exception
    {
        public UnsupportedFileTypeException(string message): base(message)
        {
        }

        public UnsupportedFileTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
