using System;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public enum ForceOriginalAspectRatio
    {
        Disable,
        Decrease,
        Increase
    }

    /// <summary>
    /// Scale (resize) the input video, using the libswscale library.
    /// 
    /// The scale filter forces the output display aspect ratio to be the same of the input, by changing the output sample aspect ratio.
    /// 
    /// If the input image format is different from the format requested by the next filter, the scale filter will convert the input to 
    /// the requested format.
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#scale
    /// </summary>
    public class ScaleFilter : FilterBase
    {
        public string Width { get; set; }

        public string Height { get; set; }

        /// <summary>
        /// Set when the expressions for x, and y are evaluated. It accepts the following values:
        /// ‘init’: only evaluate expressions once during the filter initialization or when a command is processed
        /// ‘frame’: evaluate expressions for each incoming frame 
        /// 
        /// Default value is ‘frame’. 
        /// </summary>
        public CoordinateEval? Eval { get; set; }

        /// <summary>
        /// Set the interlacing mode. It accepts the following values:
        /// ‘1’: Force interlaced aware scaling.
        /// ‘0’: Do not apply interlaced scaling.
        /// ‘-1’: Select interlaced aware scaling depending on whether the source frames are flagged as interlaced or not. 
        /// 
        /// Default value is ‘0’.
        /// </summary>
        public int? InterlaceMode { get; set; }

        /// <summary>
        /// Set libswscale scaling flags. See https://ffmpeg.org/ffmpeg-scaler.html#sws_005fflags for the complete list of values. 
        /// If not explicitly specified the filter applies the default flags. 
        /// </summary>
        public string Flags { get; set; }

        /// <summary>
        /// Set the video size. For the syntax of this option, https://ffmpeg.org/ffmpeg-utils.html#video-size-syntax
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Enable decreasing or increasing output video width or height if necessary to keep the original aspect ratio. Possible values:
        /// ‘disable’: Scale the video as specified and disable this feature.
        /// ‘decrease’: The output video dimensions will automatically be decreased if needed.
        /// ‘increase’: The output video dimensions will automatically be increased if needed.
        /// 
        /// One useful instance of this option is that when you know a specific device’s maximum allowed resolution, 
        /// you can use this to limit the output video to that, while retaining the aspect ratio. For example, 
        /// device A allows 1280x720 playback, and your video is 1920x800. Using this option (set it to decrease) and 
        /// specifying 1280x720 to the command line makes the output 1280x533.
        /// 
        /// Please note that this is a different thing than specifying -1 for w or h, you still need to specify the output 
        /// resolution for this option to work.
        /// </summary>
        public ForceOriginalAspectRatio? ForceOriginalAspectRatio { get; set; }

        public ScaleFilter()
        {
            FilterName = "scale";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(Width != null)
                paramList.Add(new CommandParam("w", Width));

            if(Height != null)
                paramList.Add(new CommandParam("h", Height));

            if(Eval.HasValue)
                paramList.Add(new CommandParam("eval", Eval.Value.ToString().ToLower()));

            if(InterlaceMode.HasValue)
            {
                int val = MathHelper.Clamp(InterlaceMode.Value, -1, 1);
                paramList.Add(new CommandParam("interl", val.ToString()));
            }

            if(Flags != null)
                paramList.Add(new CommandParam("flags", Flags));

            if(Size != null)
                paramList.Add(new CommandParam("size", Size));

            if(ForceOriginalAspectRatio.HasValue)
                paramList.Add(new CommandParam("force_original_aspect_ratio", ForceOriginalAspectRatio.Value.ToString().ToLower()));

            return paramList;
        }
    }
}