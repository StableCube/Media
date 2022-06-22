using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public class QuantizationParameterOption : Libx264OptionBase
    {
        public const string Key = "-qp";

        ///<summary>
        /// Quantization parameter rate control method (from -1 to INT_MAX) (default -1)
        ///</summary>
        public int Value { get; set; }

        public QuantizationParameterOption() : base(Key, null) {}

        public QuantizationParameterOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            int value = MathHelper.Clamp(Value, -1, int.MaxValue);

            return new CommandParam(StreamOptionKey, value.ToString());
        }
    }
}