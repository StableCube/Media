
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Audio
{
    public enum SampleFormat
    {
        u8,
        s16,
        s32,
        flt,
        dbl,
        u8p,
        s16p,
        s32p,
        fltp, 
        dblp,
    }

    public class SampleFormatOption : AudioOptionBase
    {
        public const string Key = "-sample_fmt";

        /// <summary>
        ///     Set the audio sample format. Use -sample_fmts to get a list of supported sample formats.
        /// </summary>
        public SampleFormat Format { get; set; }

        public SampleFormatOption() : base(Key, null) {}

        public SampleFormatOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Format.ToString());
        }
    }
}