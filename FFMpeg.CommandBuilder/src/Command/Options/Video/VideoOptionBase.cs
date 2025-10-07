
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Video
{
    public abstract class VideoOptionBase : StreamOptionBase, IVideoOption
    {
        public const string OptionTypeId = "Video";

        public VideoOptionBase(string key) : base(OptionTypeId, key, null) {}

        public VideoOptionBase(string key, StreamSpecifier stream) : base(OptionTypeId, key, stream) {}
    }
}