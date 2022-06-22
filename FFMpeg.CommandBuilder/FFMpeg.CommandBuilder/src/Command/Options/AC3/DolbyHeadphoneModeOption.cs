
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AC3
{
    public enum DolbyHeadphoneMode
    {
        notindicated,
        on,
        off
    }

    public class DolbyHeadphoneModeOption : AC3OptionBase
    {
        public const string Key = "-dheadphone_mode";

        /// <summary>
        /// Dolby Headphone Mode. Indicates whether the stream uses Dolby Headphone encoding (multi-channel matrixed to 2.0 for use with headphones). 
        /// Using this option does NOT mean the encoder will actually apply Dolby Headphone processing.  
        /// </summary>
        public DolbyHeadphoneMode Mode { get; set; }

        public DolbyHeadphoneModeOption() : base(Key, null) {}

        public DolbyHeadphoneModeOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Mode.ToString());
        }
    }
}