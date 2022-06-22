using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx265
{
    public class ConstantRateFactorOption : Libx265OptionBase
    {
        public const string Key = "-crf";

        ///<summary>
        /// The Constant Rate Factor (CRF) is the default quality (and rate control) setting for the x264 and x265 encoders. 
        /// You can set the values between 0 and 51, where lower values would result in better quality, at the expense of 
        /// higher file sizes. Higher values mean more compression, but at some point you will notice the quality degradation.
        /// 
        /// For x265, the default CRF is 28
        /// </summary>
        public int Value { get; set; }

        public ConstantRateFactorOption() : base(Key, null) {}

        public ConstantRateFactorOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            var clampedVal = MathHelper.Clamp(Value, 0, 51);

            return new CommandParam(StreamOptionKey, clampedVal.ToString());
        }
    }
}