
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public abstract class Libx264OptionBase : StreamOptionBase, ILibx264Option
    {
        public const string OptionTypeId = "Libx264";

        public Libx264OptionBase(string key) : base(OptionTypeId, key) {}

        public Libx264OptionBase(string key, StreamSpecifier stream) : base(OptionTypeId, key, stream) {}
    }
}