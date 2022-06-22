
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AC3
{
    public enum StereoDownmixMode
    {
        notindicated,
        ltrt,
        loro
    }

    public class StereoDownmixModeOption : AC3OptionBase
    {
        public const string Key = "-dmix_mode";

        /// <summary>
        /// Preferred Stereo Downmix Mode. Allows the user to select either Lt/Rt (Dolby Surround) 
        /// or Lo/Ro (normal stereo) as the preferred stereo downmix mode.
        /// </summary>
        public StereoDownmixMode Mode { get; set; }

        public StereoDownmixModeOption() : base(Key, null) {}

        public StereoDownmixModeOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Mode.ToString());
        }
    }
}