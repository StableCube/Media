
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class DefineOption : CommandOptionBase
    {
        public string Definition { get; set; }

        public override string Key { get { return "define"; } }

        public override string Value { get { return Definition; } }
    }
}