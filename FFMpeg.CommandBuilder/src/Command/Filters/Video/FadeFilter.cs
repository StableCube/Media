using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public enum FadeType
    {
        In,
        Out
    }

    /// <summary>
    /// Apply a fade-in/out effect to the input video. 
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#toc-fade
    /// </summary>
    public class FadeFilter : FilterBase
    {
        /// <summary>
        /// The effect type can be either "in" for a fade-in, or "out" for a fade-out effect. Default is in. 
        /// </summary>
        public FadeType? Type { get; set; }

        /// <summary>
        /// Specify the number of the frame to start applying the fade effect at. Default is 0. 
        /// </summary>
        public int? StartFrame { get; set; }

        /// <summary>
        /// The number of frames that the fade effect lasts. At the end of the fade-in effect, the output video will have the 
        /// same intensity as the input video. At the end of the fade-out transition, the output video will be filled with the 
        /// selected color. Default is 25. 
        /// </summary>
        public int? NBFrames { get; set; }

        /// <summary>
        /// If set to 1, fade only alpha channel, if one exists on the input. Default value is 0. 
        /// </summary>
        public bool? Alpha { get; set; }

        /// <summary>
        /// start_time, st
        /// 
        /// Specify the timestamp (in seconds) of the frame to start to apply the fade effect. 
        /// If both start_frame and start_time are specified, the fade will start at whichever comes last. Default is 0. 
        /// </summary>
        public int? StartTime { get; set; }

        /// <summary>
        /// duration, d
        /// 
        /// The number of seconds for which the fade effect has to last. At the end of the fade-in effect the output 
        /// video will have the same intensity as the input video, at the end of the fade-out transition the output 
        /// video will be filled with the selected color. If both duration and nb_frames are specified, duration is used. 
        /// 
        /// Default is 0 (nb_frames is used by default).
        /// </summary>
        public double? Duration { get; set; }

        /// <summary>
        /// color, c
        /// 
        /// Specify the color of the fade. Default is "black". 
        /// </summary>
        public string Color { get; set; }

        public FadeFilter()
        {
            FilterName = "fade";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(Type.HasValue)
                paramList.Add(new CommandParam("type", Type.Value.ToString().ToLower()));

            if(StartFrame.HasValue)
                paramList.Add(new CommandParam("start_frame", StartFrame.ToString()));

            if(NBFrames.HasValue)
                paramList.Add(new CommandParam("nb_frames", NBFrames.ToString()));

            if(Alpha.HasValue)
                paramList.Add(new CommandParam("alpha", (Alpha.Value == true ? "1" : "0")));

            if(StartTime.HasValue)
                paramList.Add(new CommandParam("start_time",  StartTime.ToString()));

            if(Duration.HasValue)
                paramList.Add(new CommandParam("duration", Duration.ToString()));

            if(Color != null)
                paramList.Add(new CommandParam("color",  Color));

            return paramList;
        }
    }
}