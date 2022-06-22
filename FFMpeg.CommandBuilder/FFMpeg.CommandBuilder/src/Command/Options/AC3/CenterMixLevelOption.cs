using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AC3
{
    public class CenterMixLevelOption : AC3OptionBase
    {
        public const string Key = "-center_mixlev";

        /// <summary>
        /// Center Mix Level. The amount of gain the decoder should apply to the center channel when downmixing to stereo. 
        /// This field will only be written to the bitstream if a center channel is present. 
        /// The value is specified as a scale factor.
        /// </summary>
        public float Value { get; set; }

        public CenterMixLevelOption() : base(Key, null) {}

        public CenterMixLevelOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            if(Value != 0.707 && Value != 0.595 && Value != 0.500)
                throw new ArgumentOutOfRangeException("CenterMixLevel");

            return new CommandParam(StreamOptionKey, Value.ToString());
        }
    }
}