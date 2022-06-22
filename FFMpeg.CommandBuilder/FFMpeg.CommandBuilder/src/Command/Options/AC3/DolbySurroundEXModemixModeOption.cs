
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AC3
{
    public enum DolbySurroundEXModemixMode
    {
        notindicated,
        on,
        off
    }

    public class DolbySurroundEXModemixModeOption : AC3OptionBase
    {
        public const string Key = "-dsurex_mode";

        /// <summary>
        /// Dolby Surround EX Mode. Indicates whether the stream uses Dolby Surround EX (7.1 matrixed to 5.1). 
        /// Using this option does NOT mean the encoder will actually apply Dolby Surround EX processing. 
        /// </summary>
        public DolbySurroundEXModemixMode Mode { get; set; }

        public DolbySurroundEXModemixModeOption() : base(Key, null) {}

        public DolbySurroundEXModemixModeOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Mode.ToString());
        }
    }
}