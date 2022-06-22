using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Loop video frames.
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#toc-loop
    ///  </summary>
    public class LoopFilter : FilterBase
    {
        /// <summary>
        /// Set the number of loops. Setting this value to -1 will result in infinite loops. Default is 0.
        /// </summary>
        public int? Loop { get; set; }

        /// <summary>
        /// Set maximal size in number of frames. Default is 0. 
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Set first frame of loop. Default is 0. 
        /// </summary>
        public int? Start { get; set; }

        public LoopFilter()
        {
            FilterName = "loop";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(Loop.HasValue)
                paramList.Add(new CommandParam("loop", Loop.Value.ToString()));

            if(Size.HasValue)
                paramList.Add(new CommandParam("size", Size.Value.ToString()));

            if(Start.HasValue)
                paramList.Add(new CommandParam("start", Start.Value.ToString()));

            return paramList;
        }
    }
}