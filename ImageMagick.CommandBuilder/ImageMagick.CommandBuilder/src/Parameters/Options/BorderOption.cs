
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class BorderOption : CommandOptionBase
    {
        public int Size { get; set; }

        public override string Key { get { return "border"; } }

        public override string Value { get { return Size.ToString(); } }
    }
}