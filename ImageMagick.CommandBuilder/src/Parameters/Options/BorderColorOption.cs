
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class BorderColorOption : CommandOptionBase
    {
        public ImageColor Color { get; set; }

        public override string Key { get { return "bordercolor"; } }

        public override string Value { get { return Color.ColorString; } }
    }
}