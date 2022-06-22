
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class GeometryOption : CommandOptionBase
    {
        public ImageGeometry Geometry { get; set; }

        public override string Key { get { return "geometry"; } }

        public override string Value { get { return Geometry.ToString(); } }
    }
}