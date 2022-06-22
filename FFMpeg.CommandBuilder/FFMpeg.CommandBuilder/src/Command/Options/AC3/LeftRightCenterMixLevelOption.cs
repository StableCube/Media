
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AC3
{
    public class LeftRightCenterMixLevelOption : AC3OptionBase
    {
        public const string Key = "-ltrt_cmixlev";

        /// <summary>
        /// Lt/Rt Center Mix Level. The amount of gain the decoder should apply to the center channel when downmixing to stereo in Lt/Rt mode.
        /// 0.595 | Apply -4.5dB gain (default) 
        /// </summary>
        public float Level { get; set; }

        public LeftRightCenterMixLevelOption() : base(Key, null) {}

        public LeftRightCenterMixLevelOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Level.ToString());
        }
    }
}