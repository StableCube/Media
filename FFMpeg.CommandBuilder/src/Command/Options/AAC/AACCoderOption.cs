
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AAC
{
    public enum EncodingMethod
    {
        TwoLoop,
        ANMR,
        Fast,
    }

    public class AACCoderOption : AACOptionBase
    {
        public const string Key = "-aac_coder";

        /// <summary>
        ///     Set AAC encoder coding method. Possible values:
        /// 
        ///     ‘twoloop’
        ///         Two loop searching (TLS) method.
        /// 
        ///         This method first sets quantizers depending on band thresholds and then tries to find an optimal combination 
        ///         by adding or subtracting a specific value from all quantizers and adjusting some individual quantizer a little. 
        ///         Will tune itself based on whether aac_is, aac_ms and aac_pns are enabled.
        /// 
        ///     ‘anmr’
        ///         Average noise to mask ratio (ANMR) trellis-based solution.
        /// 
        ///         This is an experimental coder which currently produces a lower quality, is more unstable and is slower than 
        ///         the default twoloop coder but has potential. Currently has no support for the aac_is or aac_pns options. 
        ///         Not currently recommended.
        /// 
        ///     ‘fast’
        ///         Constant quantizer method.
        /// 
        ///         Uses a cheaper version of twoloop algorithm that doesn’t try to do as many clever adjustments. 
        ///         Worse with low bitrates (less than 64kbps), but is better and much faster at higher bitrates. 
        ///         This is the default choice for a coder
        /// </summary>
        public EncodingMethod Method { get; set; }

        public AACCoderOption() : base(Key, null) {}

        public AACCoderOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam($"{OptionKey}:{Stream.ToString()}", Method.ToString().ToLower());
        }
    }
}