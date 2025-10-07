
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class QualityOption : CommandOptionBase
    {
        public int Quality { get; set; }

        public override string Key { get { return "quality"; } }

        public override string Value { get { return Quality.ToString(); } }
    }
}