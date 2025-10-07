
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Muxer
{
    public class StartNumberOption : MuxerOptionBase
    {
        public const string Key = "-start_number";

        /// <summary>
        /// Start the sequence from the specified number. 
        /// Default value is 1. 
        /// </summary>
        public int Number { get; set; }

        public StartNumberOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, Number.ToString());
        }
    }
}