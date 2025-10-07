
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class CodecOption : MainOptionBase
    {
        public const string Key = "-codec";

        public string CodecName { get; set; }

        public CodecOption() : base(Key, null) {}

        public CodecOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, CodecName);
        }
    }
}