using System;

namespace StableCube.Media.ImageMagick
{
    public class BinaryPathConfig
    {
        public string MagickBinary { get; set; } = "/usr/bin/magick";
        
        public string IdentifyBinary { get; set; } = "/usr/bin/identify";

        public string ConvertBinary { get; set; } = "/usr/bin/convert";
    }
}
