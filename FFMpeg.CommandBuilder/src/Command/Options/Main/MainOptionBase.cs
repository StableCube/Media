
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public abstract class MainOptionBase : StreamOptionBase, IMainOption
    {
        public const string OptionTypeId = "Main";

        public MainOptionBase(string key) : base(OptionTypeId, key){}

        public MainOptionBase(string key, StreamSpecifier stream) : base(OptionTypeId, key, stream){}
    }
}