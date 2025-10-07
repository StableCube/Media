
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Video
{
    public class FramerateOption : VideoOptionBase
    {
        public const string Key = "-r";

        /// <summary>
        ///     Set frame rate (Hz value, fraction or abbreviation).
        ///     As an input option, ignore any timestamps stored in the file and instead generate timestamps assuming constant frame rate fps. 
        ///     This is not the same as the -framerate option used for some input formats like image2 or v4l2 
        ///     (it used to be the same in older versions of FFmpeg). If in doubt use -framerate instead of the input option -r.
        ///     As an output option, duplicate or drop input frames to achieve constant output frame rate fps.
        /// </summary>
        public float FrameRate { get; set; }

        public FramerateOption() : base(Key, null) {}

        public FramerateOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, FrameRate.ToString());
        }
    }
}