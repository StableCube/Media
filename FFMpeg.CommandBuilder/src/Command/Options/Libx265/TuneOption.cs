
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx265
{
    public enum Tune
    {
        PSNR,
        SSIM,
        Grain,
        ZeroLatency,
        FastDecode
    }

    public class TuneOption : Libx265OptionBase
    {
        public const string Key = "-tune";

        ///<summary>
        /// Choose a tune. By default, this is disabled, and it is generally not required to set a tune option. 
        /// x265 supports the following -tune options: psnr, ssim, grain, zerolatency, fastdecode
        ///</summary>
        public Tune Value { get; set; }

        public TuneOption() : base(Key, null) {}

        public TuneOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Value.ToString().ToLower());
        }
    }
}