using System.Collections.Generic;

namespace StableCube.Media.ImageMagick.DataModel
{
    /// <summary>
    /// The raw 1:1 map to ImageMagick "image" identify result
    /// </summary>
    public class ImageMagickImage
    {
        public string Name { get; set; }

        public string Format { get; set; }

        public string FormatDescription { get; set; }

        public string MimeType { get; set; }

        public string Class { get; set; }

        public ImageInfoGeometry Geometry { get; set; }

        public string Units { get; set; }

        public string Type { get; set; }

        public string Endianess { get; set; }

        public string Colorspace { get; set; }

        public int Depth { get; set; }

        public int BaseDepth { get; set; }

        public ImageInfoChannelDepth ChannelDepth { get; set; }

        public uint Pixels { get; set; }

        public ImageInfoGeometry PageGeometry { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public string[] ColorMap { get; set; }

        public ImageStatistics ImageStatistics { get; set; }

        public ChannelStatistics ChannelStatistics { get; set; }

        public string RenderingIntent { get; set; }

        public float Gamma { get; set; }

        public string MatteColor { get; set; }

        public string BackgroundColor { get; set; }
        
        public string BorderColor { get; set; }

        public string TransparentColor { get; set; }

        public string Interlace { get; set; }

        public string Intensity { get; set; }

        public string Compose { get; set; }

        public string Dispose { get; set; }

        public string Delay { get; set; }

        public int Iterations { get; set; }

        public int Scene { get; set; }

        public int Scenes { get; set; }

        public string Compression { get; set; }

        public string Orientation { get; set; }

        public bool Tainted { get; set; }

        public string Filesize { get; set; }

        public string NumberPixels { get; set; }

        public string PixelsPerSecond { get; set; }

        public string UserTime { get; set; }

        public string ElapsedTime { get; set; }

        public string Version { get; set; }

        public Chromaticity Chromaticity { get; set; }

        public int Quality { get; set; }
    }
}