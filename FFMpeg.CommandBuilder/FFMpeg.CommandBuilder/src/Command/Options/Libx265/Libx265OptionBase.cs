
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx265
{
    public abstract class Libx265OptionBase : StreamOptionBase, ILibx265Option
    {
        public const string OptionTypeId = "Libx265";

        public Libx265OptionBase(string key) : base(OptionTypeId, key) {}

        public Libx265OptionBase(string key, StreamSpecifier stream) : base(OptionTypeId, key, stream) {}
    }
}