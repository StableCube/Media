
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Video
{
    public class PassOption : VideoOptionBase
    {
        public const string Key = "-pass";

        /// <summary>
        ///     Select the pass number (1 or 2). It is used to do two-pass video encoding. 
        ///     The statistics of the video are recorded in the first pass into a log file (see also the option -passlogfile), and 
        ///     in the second pass that log file is used to generate the video at the exact requested bitrate. 
        ///     On pass 1, you may just deactivate audio and set output to null, examples for Windows and Unix
        /// <summary>
        public int PassNumber { get; set; }

        public PassOption() : base(Key, null) {}

        public PassOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, PassNumber.ToString());
        }
    }
}