
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Audio
{
    public class ACodecOption : AudioOptionBase
    {
        public const string Key = "-acodec";

        public string Codec { get; set; }

        public ACodecOption() : base(Key, null) {}

        public ACodecOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, Codec);
        }
    }
}