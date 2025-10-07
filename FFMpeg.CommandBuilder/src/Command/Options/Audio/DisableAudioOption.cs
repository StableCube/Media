
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Audio
{
    public class DisableAudioOption : AudioOptionBase
    {
        public const string Key = "-an";

        public DisableAudioOption() : base(Key) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, null);
        }
    }
}