
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Generic
{
    public class OverwriteOutputNoOption : GenericOptionBase
    {
        public const string Key = "-n";

        public OverwriteOutputNoOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, null);
        }
    }
}