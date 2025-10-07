
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class ResizeOption : CommandOptionBase
    {
        public ImageGeometry Geometry { get; set; } = new ImageGeometry();

        public override string Key { get { return "resize"; } }

        public override string Value { get { return Geometry.ToString(); } }
    }
}