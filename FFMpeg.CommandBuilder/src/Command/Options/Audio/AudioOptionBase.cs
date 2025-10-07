
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Audio
{
    public abstract class AudioOptionBase : StreamOptionBase, IAudioOption
    {
        public const string OptionTypeId = "Audio";

        public AudioOptionBase(string key) : base(OptionTypeId, key) {}

        public AudioOptionBase(string key, StreamSpecifier stream) : base(OptionTypeId, key, stream) {}
    }
}