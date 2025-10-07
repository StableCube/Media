
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class ComposeOption : CommandOptionBase
    {
        public CompositionMethod Compose { get; set; } = new CompositionMethod();

        public override string Key { get { return "compose"; } }

        public override string Value { get { return Compose.ToString(); } }
    }
}