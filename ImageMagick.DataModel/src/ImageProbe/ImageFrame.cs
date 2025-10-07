
namespace StableCube.Media.ImageMagick.DataModel
{
    public class ImageFrame
    {
        public int FrameIndex { get; set; }

        public ImageInfoGeometry PageGeometry { get; set; }

        public string Units { get; set; }

        public string Type { get; set; }

        public string Colorspace { get; set; }

        public int Depth { get; set; }

        public int BaseDepth { get; set; }

        public ImageInfoChannelDepth ChannelDepth { get; set; }

        public uint Pixels { get; set; }

        public string[] ColorMap { get; set; }

        public ImageStatistics ImageStatistics { get; set; }

        public ChannelStatistics ChannelStatistics { get; set; }

        public string RenderingIntent { get; set; }

        public float Gamma { get; set; }

        public string MatteColor { get; set; }

        public string BackgroundColor { get; set; }
        
        public string BorderColor { get; set; }

        public string TransparentColor { get; set; }

        public string Intensity { get; set; }

        public string Compose { get; set; }

        public string Dispose { get; set; }

        public string Orientation { get; set; }

        public bool Tainted { get; set; }

        public string Filesize { get; set; }

        public string NumberPixels { get; set; }

        public string PixelsPerSecond { get; set; }

        public string UserTime { get; set; }

        public string ElapsedTime { get; set; }

        public Chromaticity Chromaticity { get; set; }

        public int Quality { get; set; }
    }
}