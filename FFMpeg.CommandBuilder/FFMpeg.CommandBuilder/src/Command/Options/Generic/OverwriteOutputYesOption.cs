
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Generic
{
    public class OverwriteOutputYesOption : GenericOptionBase
    {
        public const string Key = "-y";

        public OverwriteOutputYesOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, null);
        }
    }
}