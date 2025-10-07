using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AC3
{
    public class SurroundMixLevelOption : AC3OptionBase
    {
        public const string Key = "-surround_mixlev";

        /// <summary>
        /// Surround Mix Level. The amount of gain the decoder should apply to the surround channel(s) when downmixing to stereo. 
        /// This field will only be written to the bitstream if one or more surround channels are present. 
        /// The value is specified as a scale factor.
        /// </summary>
        public float Level { get; set; }

        public SurroundMixLevelOption() : base(Key, null) {}

        public SurroundMixLevelOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            if(Level != 0.707 && Level != 0.500 && Level != 0.000 )
                throw new ArgumentOutOfRangeException("SurroundMixLevelOption");

            return new CommandParam(StreamOptionKey, Level.ToString());
        }
    }
}