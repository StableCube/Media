using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public class PFrameWeightedPredictionOption : Libx264OptionBase
    {
        public const string Key = "-wpredp";

        ///<summary>Weighted prediction for P-frames</summary>
        public int Value { get; set; }

        public PFrameWeightedPredictionOption() : base(Key, null) {}

        public PFrameWeightedPredictionOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            var value = MathHelper.Clamp(Value, -1, int.MaxValue);

            return new CommandParam(StreamOptionKey, value.ToString());
        }
    }
}