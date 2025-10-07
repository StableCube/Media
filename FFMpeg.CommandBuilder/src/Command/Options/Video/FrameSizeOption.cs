using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Video
{
    public class FrameSizeOption : VideoOptionBase
    {
        public const string Key = "-s";

        /// <summary>
        ///     Set frame size.
        ///     As an input option, this is a shortcut for the video_size private option, recognized by some demuxers for which the 
        ///     frame size is either not stored in the file or is configurable – e.g. raw video or video grabbers.
        ///     As an output option, this inserts the scale video filter to the end of the corresponding filtergraph. 
        ///     Please use the scale filter directly to insert it at the beginning or some other place.
        ///     The format is ‘wxh’ (default - same as source).
        /// </summary>
        public int Width { get; set; }

        public int Height { get; set; }

        public FrameSizeOption() : base(Key, null) {}

        public FrameSizeOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            var width = ((int)Math.Round(Width / 2.0)) * 2;
            var height = ((int)Math.Round(Height / 2.0)) * 2;

            return new CommandParam(StreamOptionKey, $"{width.ToString()}x{height.ToString()}");
        }
    }
}