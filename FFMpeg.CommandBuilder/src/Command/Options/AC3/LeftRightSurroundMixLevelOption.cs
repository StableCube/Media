
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AC3
{
    public class LeftRightSurroundMixLevelOption : AC3OptionBase
    {
        public const string Key = "-ltrt_surmixlev";

        /// <summary>
        /// Lt/Rt Surround Mix Level. The amount of gain the decoder should apply to the surround channel(s) when downmixing 
        /// to stereo in Lt/Rt mode. 
        /// </summary>
        public float Level { get; set; }

        public LeftRightSurroundMixLevelOption() : base(Key, null) {}

        public LeftRightSurroundMixLevelOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Level.ToString());
        }
    }
}