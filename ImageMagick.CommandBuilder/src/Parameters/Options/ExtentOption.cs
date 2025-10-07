
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class ExtentOption : CommandOptionBase
    {
        public ImageDimensions Dimensions { get; set; }

        public override string Key { get { return "extent"; } }

        public override string Value { get { return string.Format("{0}x{1}", Dimensions.Width, Dimensions.Height); } }
    }
}