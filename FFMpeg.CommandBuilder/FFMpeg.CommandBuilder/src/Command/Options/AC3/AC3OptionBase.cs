
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AC3
{
    public abstract class AC3OptionBase : StreamOptionBase, IAC3Option
    {
        public const string OptionTypeId = "AC3";

        public AC3OptionBase(string key) : base(OptionTypeId, key) {}

        public AC3OptionBase(string key, StreamSpecifier stream) : base(OptionTypeId, key, stream) {}
    }
}