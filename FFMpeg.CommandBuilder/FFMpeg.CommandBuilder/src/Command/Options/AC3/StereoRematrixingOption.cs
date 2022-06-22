
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AC3
{
    public class StereoRematrixingOption : AC3OptionBase
    {
        public const string Key = "-stereo_rematrixing";

        /// <summary>
        /// Stereo Rematrixing. Enables/Disables use of rematrixing for stereo input. 
        /// This is an optional AC-3 feature that increases quality by selectively encoding the left/right channels as mid/side. 
        /// This option is enabled by default, and it is highly recommended that it be left as enabled except for testing purposes.  
        /// </summary>
        public bool Enabled { get; set; }

        public StereoRematrixingOption() : base(Key, null) {}

        public StereoRematrixingOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Enabled.ToString());
        }
    }
}