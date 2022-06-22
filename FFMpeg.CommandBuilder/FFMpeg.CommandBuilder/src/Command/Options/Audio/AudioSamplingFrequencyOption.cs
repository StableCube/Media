
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Audio
{
    public class AudioSamplingFrequencyOption : AudioOptionBase
    {
        public const string Key = "-ar";

        /// <summary>
        ///     Set the audio sampling frequency. For output streams it is set by default to the frequency 
        ///     of the corresponding input stream. For input streams this option only makes sense for audio 
        ///     grabbing devices and raw demuxers and is mapped to the corresponding demuxer options.
        /// </summary>
        public string Frequency { get; set; }

        public AudioSamplingFrequencyOption() : base(Key, null) {}

        public AudioSamplingFrequencyOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Frequency);
        }
    }
}