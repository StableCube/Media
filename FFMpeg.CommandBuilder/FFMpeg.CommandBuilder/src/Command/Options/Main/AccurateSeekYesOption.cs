
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class AccurateSeekYesOption : MainOptionBase
    {
        public const string Key = "-accurate_seek";

        public AccurateSeekYesOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, null);
        }
    }
}