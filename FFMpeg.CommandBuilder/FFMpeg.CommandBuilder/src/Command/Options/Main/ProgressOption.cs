
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class ProgressOption : MainOptionBase
    {
        public const string Key = "-progress";

        /// <summary>
        ///     Send program-friendly progress information to url.
        ///     Progress information is written approximately every second and at the end of the encoding process. 
        ///     It is made of "key=value" lines. key consists of only alphanumeric characters. 
        ///     The last key of a sequence of progress information is always "progress".
        /// </summary>
        public string FilePath { get; set; }

        public ProgressOption () : base (Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, FilePath);
        }
    }
}