using System;

namespace StableCube.Media.FFMpeg.CommandFactory
{
    public static class VideoMiscMapper
    {
        public static string ImageTypeToExtension(ImageFormatType imgType)
        {
            switch (imgType)
            {
                case ImageFormatType.Jpeg:
                    return "jpg";
                case ImageFormatType.Gif:
                    return "gif";
                case ImageFormatType.Png:
                    return "png";
                case ImageFormatType.WebP:
                    return "webp";

                default:
                    throw new ArgumentOutOfRangeException($"ImageFormatType not known {imgType.ToString()}");
            }
        }
    }
}