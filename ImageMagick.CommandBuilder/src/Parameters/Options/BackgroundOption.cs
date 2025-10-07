
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class BackgroundOption : CommandOptionBase
    {
        public ImageColor Color { get; set; }

        public override string Key { get { return "background"; } }

        public override string Value { get { return Color.ColorString; } }
    }
}