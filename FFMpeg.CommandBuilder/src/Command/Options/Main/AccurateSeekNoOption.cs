
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class AccurateSeekNoOption : MainOptionBase
    {
        public const string Key = "-noaccurate_seek";

        public AccurateSeekNoOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, null);
        }
    }
}