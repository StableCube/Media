
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class FramesOption : MainOptionBase
    {
        public const string Key = "-frames";

        /// <summary>
        /// Stop writing to the stream after framecount frames. 
        /// </summary>
        public int FrameCount { get; set; }

        public FramesOption() : base(Key, null) {}

        public FramesOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, FrameCount.ToString());
        }
    }
}