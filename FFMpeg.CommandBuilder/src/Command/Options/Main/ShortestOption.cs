
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class ShortestOption : MainOptionBase
    {
        public const string Key = "-shortest";

        public ShortestOption() : base (Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, null);
        }
    }
}