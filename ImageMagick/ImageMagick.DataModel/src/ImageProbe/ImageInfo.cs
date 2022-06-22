using System.Collections.Generic;

namespace StableCube.Media.ImageMagick.DataModel
{
    public enum ImageType
    {
        Gif,
        Jpg,
        Png,
        Bmp,
        Tiff,
        Webp,
        Invalid
    }

    public class ImageInfo
    {
        public bool IsAnimated { get { return (Frames.Length > 1); } }

        public bool HasTransparency 
        { 
            get 
            {
                foreach (var frame in Frames)
                {
                    if(frame.ChannelStatistics.Alpha != null)
                        return true;
                }

                return false; 
            } 
        }

        public ImageType ImageType 
        { 
            get 
            {
                return FormatToType(Format); 
            } 
        }

        public string Path { get; set; }

        public string Endianess { get; set; }

        public string Format { get; set; }

        public string FormatDescription { get; set; }

        public string MimeType { get; set; }

        public string Class { get; set; }

        public ImageInfoGeometry Geometry { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public string Version { get; set; }

        public int FrameCount { get; set; }

        public int Iterations { get; set; }

        public string Delay { get; set; }

        public string Interlace { get; set; }

        public string Compression { get; set; }

        public ImageFrame[] Frames { get; set; }

        private static ImageType FormatToType(string format)
        {
            switch (format.ToLower())
            {
                case "gif":
                    return ImageType.Gif;

                case "png":
                    return ImageType.Png;

                case "png8":
                    return ImageType.Png;

                case "png00":
                    return ImageType.Png;

                case "png24":
                    return ImageType.Png;

                case "png32":
                    return ImageType.Png;

                case "png48":
                    return ImageType.Png;

                case "png64":
                    return ImageType.Png;

                case "jpeg":
                    return ImageType.Jpg;

                case "tiff":
                    return ImageType.Tiff;

                case "bmp":
                    return ImageType.Bmp;

                case "webp":
                    return ImageType.Webp;

                default:
                    return ImageType.Invalid;   
            }
        }
    }
}