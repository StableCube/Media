using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Select the most representative frame in a given sequence of consecutive frames.
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#thumbnail
    ///  </summary>
    public class ThumbnailFilter : FilterBase
    {
        /// <summary>
        /// Set the frames batch size to analyze; in a set of n frames, the filter will pick one of them, 
        /// and then handle the next batch of n frames until the end. Default is 100. 
        /// </summary>
        public int? BatchSize { get; set; }

        public ThumbnailFilter()
        {
            FilterName = "thumbnail";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(BatchSize.HasValue)
            {
                paramList.Add(new CommandParam(BatchSize.Value.ToString(), null));
            }
            else
            {
                paramList.Add(new CommandParam("100", null));
            }

            return paramList;
        }
    }
}