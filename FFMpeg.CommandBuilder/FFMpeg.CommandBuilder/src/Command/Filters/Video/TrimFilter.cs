using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Trim the input so that the output contains one continuous subpart of the input. 
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#toc-trim
    ///  </summary>
    public class TrimFilter : FilterBase
    {
        /// <summary>
        /// Specify the time of the start of the kept section, i.e. the frame with the timestamp start will be the first frame in the output. 
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// Specify the time of the first frame that will be dropped, i.e. the frame immediately preceding 
        /// the one with the timestamp end will be the last frame in the output. 
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// This is the same as start, except this option sets the start timestamp in timebase units instead of seconds. 
        /// </summary>
        public string StartPTS { get; set; }

        /// <summary>
        /// This is the same as end, except this option sets the end timestamp in timebase units instead of seconds. 
        /// </summary>
        public string EndPTS { get; set; }

        /// <summary>
        /// The maximum duration of the output in seconds. 
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// The number of the first frame that should be passed to the output. 
        /// </summary>
        public int? StartFrame { get; set; }

        /// <summary>
        /// The number of the first frame that should be dropped. 
        /// </summary>
        public int? EndFrame { get; set; }

        public TrimFilter()
        {
            FilterName = "trim";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(Start != null)
                paramList.Add(new CommandParam("start", Start));

            if(End != null)
                paramList.Add(new CommandParam("end", End));

            if(StartPTS != null)
                paramList.Add(new CommandParam("start_pts", StartPTS));

            if(EndPTS != null)
                paramList.Add(new CommandParam("end_pts", EndPTS));

            if(Duration != null)
                paramList.Add(new CommandParam("duration", Duration));

            if(StartFrame.HasValue)
                paramList.Add(new CommandParam("start_frame", StartFrame.Value.ToString()));

            if(EndFrame.HasValue)
                paramList.Add(new CommandParam("end_frame", EndFrame.Value.ToString()));

            return paramList;
        }
    }
}