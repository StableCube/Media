
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Select frames to pass in output. 
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#toc-select_002c-aselect
    ///  </summary>
    public class ASelectFilter : SelectFilter
    {
        public ASelectFilter()
        {
            FilterName = "aselect";
        }
    }
}