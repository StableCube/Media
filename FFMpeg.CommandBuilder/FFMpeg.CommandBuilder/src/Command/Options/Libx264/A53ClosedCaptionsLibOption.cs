
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public class A53ClosedCaptionsOption : Libx264OptionBase
    {
        public const string Key = "-a53cc";

        ///<summary>Use A53 Closed Captions (if available) (default false)</summary>
        public bool Enabled { get; set; }

        public A53ClosedCaptionsOption() : base(Key, null) {}

        public A53ClosedCaptionsOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Enabled.ToString());
        }
    }
}