
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Muxer
{
    public class StringFormatTimeOption : MuxerOptionBase
    {
        public const string Key = "-strftime";

        /// <summary>
        /// If set to 1, expand the filename with date and time information from strftime(). 
        /// Default value is 0.   
        /// </summary>
        public bool Enabled { get; set; }

        public StringFormatTimeOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            string strVal = "0";
            if(Enabled)
                strVal = "1";

            return new CommandParam(OptionKey, strVal);
        }
    }
}