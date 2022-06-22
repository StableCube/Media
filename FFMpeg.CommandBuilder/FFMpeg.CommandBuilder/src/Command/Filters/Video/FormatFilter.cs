using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Convert the input video to one of the specified pixel formats. 
    /// Libavfilter will try to pick one that is suitable as input to the next filter. 
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#toc-format-1
    ///  </summary>
    public class FormatFilter : FilterBase
    {
        /// <summary>
        /// A ’|’-separated list of pixel format names, such as "pix_fmts=yuv420p|monow|rgb24". 
        /// </summary>
        public string PixelFormats { get; set; }

        public FormatFilter()
        {
            FilterName = "format";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(PixelFormats != null)
                paramList.Add(new CommandParam("pix_fmts", PixelFormats));

            return paramList;
        }
    }
}