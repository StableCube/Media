
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class AlphaOption : CommandOptionBase
    {
        public string Type { get; set; }

        public override string Key { get { return "alpha"; } }

        public override string Value { get { return Type; } }
    }
}