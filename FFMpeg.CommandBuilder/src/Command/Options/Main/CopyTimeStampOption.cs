
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class CopyTimeStampOption : MainOptionBase
    {
        public const string Key = "-copyts";

        public CopyTimeStampOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, null);
        }
    }
}