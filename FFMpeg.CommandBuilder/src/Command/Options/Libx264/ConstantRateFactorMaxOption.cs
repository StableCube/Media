using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public class ConstantRateFactorMaxOption : Libx264OptionBase
    {
        public const string Key = "-crf_max";

        ///<summary>
        ///In CRF mode, prevents VBV from lowering quality beyond this point.
        ///</summary>
        public int Value { get; set; }

        public ConstantRateFactorMaxOption() : base(Key, null) {}

        public ConstantRateFactorMaxOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            int rate = MathHelper.Clamp(Value, 0, 51);

            return new CommandParam(StreamOptionKey, rate.ToString());
        }
    }
}