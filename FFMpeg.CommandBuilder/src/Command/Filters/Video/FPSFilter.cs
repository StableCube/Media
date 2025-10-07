using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public enum FPSTimestampRounding
    {
        Zero,
        Inf,
        Down,
        Up,
        Near,
    }

    public enum FPSEndOfFileAction
    {
        Round,
        Pass,
    }

    /// <summary>
    /// Convert the video to specified constant frame rate by duplicating or dropping frames as necessary. 
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#toc-fps-1
    ///  </summary>
    public class FPSFilter : FilterBase
    {
        /// <summary>
        /// The desired output frame rate. The default is 25. 
        /// </summary>
        public double? FPS { get; set; }

        /// <summary>
        /// Assume the first PTS should be the given value, in seconds. This allows for padding/trimming at the start of stream. 
        /// By default, no assumption is made about the first frame’s expected PTS, so no padding or trimming is done. 
        /// For example, this could be set to 0 to pad the beginning with duplicates of the first frame if a video stream 
        /// starts after the audio stream or to trim any frames with a negative PTS. 
        /// </summary>
        public double? StartTime { get; set; }

        /// <summary>
        /// Timestamp (PTS) rounding method. Possible values are:
        /// zero: round towards 0 
        /// inf: round away from 0 
        /// down: round towards -infinity 
        /// up: round towards +infinity 
        /// near: round to nearest 
        /// 
        /// The default is near.
        /// </summary>
        public FPSTimestampRounding? Round { get; set; }

        public FPSEndOfFileAction? EndOfFileAction { get; set; }

        public FPSFilter()
        {
            FilterName = "fps";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(FPS.HasValue)
                paramList.Add(new CommandParam("fps", FPS.Value.ToString("G17")));

            if(StartTime.HasValue)
                paramList.Add(new CommandParam("start_time", StartTime.Value.ToString("G17")));

            if(Round.HasValue)
                paramList.Add(new CommandParam("round", Round.Value.ToString().ToLower()));

            if(EndOfFileAction.HasValue)
                paramList.Add(new CommandParam("eof_action", EndOfFileAction.Value.ToString().ToLower()));

            return paramList;
        }
    }
}