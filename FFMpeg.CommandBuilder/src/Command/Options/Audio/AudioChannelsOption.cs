
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Audio
{
    public class AudioChannelsOption : AudioOptionBase
    {
        public const string Key = "-ac";

        /// <summary>
        ///     Set the number of audio channels. For output streams it is set by default to the number of 
        ///     input audio channels. For input streams this option only makes sense for audio grabbing 
        ///     devices and raw demuxers and is mapped to the corresponding demuxer options.
        /// </summary> 
        public int ChannelCount { get; set; }

        public AudioChannelsOption() : base(Key, null) {}

        public AudioChannelsOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, ChannelCount.ToString());
        }
    }
}