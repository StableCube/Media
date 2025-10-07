
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.LibvpxVp9
{
    public abstract class LibvpxVp9OptionBase : StreamOptionBase, ILibvpxVp9Option
    {
        public const string OptionTypeId = "LibvpxVp9";

        public LibvpxVp9OptionBase(string key) : base(OptionTypeId, key) {}

        public LibvpxVp9OptionBase(string key, StreamSpecifier stream) : base(OptionTypeId, key, stream) {}
    }
}