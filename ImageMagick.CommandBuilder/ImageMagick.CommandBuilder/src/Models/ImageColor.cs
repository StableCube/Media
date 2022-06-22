
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class ImageColor
    {
        /// <summary>
        /// This option accepts a color name, a hex color, or a numerical RGB, RGBA, HSL, HSLA, CMYK, or CMYKA specification.
        /// 
        /// For example,
        /// -fill blue
        /// -fill "#ddddff"
        /// -fill "rgb(255,255,255)"
        /// </summary>
        public string ColorString { get; set; }

        public ImageColor()
        {
        }

        public ImageColor(string color)
        {
            ColorString = color;
        }

        public ImageColor(byte r, byte g, byte b)
        {
            ColorString = $"rgb({r},{g},{b})";
        }

        public ImageColor(byte r, byte g, byte b, byte a)
        {
            ColorString = $"rgba({r},{g},{b},{a})";
        }
    }
}