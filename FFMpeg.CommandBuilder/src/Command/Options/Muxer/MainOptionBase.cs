
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Muxer
{
    public abstract class MuxerOptionBase : OptionBase, IMuxerOption
    {
        public const string OptionTypeId = "Muxer";

        public MuxerOptionBase(string key) : base(OptionTypeId, key){}
    }
}