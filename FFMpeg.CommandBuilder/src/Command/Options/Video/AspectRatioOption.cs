
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Video
{
    public class AspectRatioOption : VideoOptionBase
    {
        public const string Key = "-aspect";

        /// <summary>
        ///     Set the video display aspect ratio specified by aspect.
        ///     aspect can be a floating point number string, or a string of the form num:den, where num and den are the 
        ///     numerator and denominator of the aspect ratio. For example "4:3", "16:9", "1.3333", and "1.7777" are valid argument values.
        ///     If used together with -vcodec copy, it will affect the aspect ratio stored at container level, but not the aspect 
        ///     ratio stored in encoded frames, if it exists.
        /// </summary>
        public string AspectRatio { get; set; }

        public AspectRatioOption() : base(Key, null) {}

        public AspectRatioOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, AspectRatio);
        }
    }
}