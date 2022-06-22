
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AAC
{
    public abstract class AACOptionBase : StreamOptionBase, IAACOption
    {
        public const string OptionTypeId = "AAC";

        public AACOptionBase(string key) : base(OptionTypeId, key) {}

        public AACOptionBase(string key, StreamSpecifier stream) : base(OptionTypeId, key, stream) {}
    }
}