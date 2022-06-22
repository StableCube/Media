
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class GravityOption : CommandOptionBase
    {
        public enum GravityType
        {
            NorthWest, North, NorthEast, West, Center, East, SouthWest, South, SouthEast
        }

        public GravityType Gravity { get; set; }

        public override string Key { get { return "gravity"; } }

        public override string Value { get { return Gravity.ToString(); } }
    }
}