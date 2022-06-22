using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Select frames to pass in output. 
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#toc-select_002c-aselect
    ///  </summary>
    public class SelectFilter : FilterBase
    {
        public string Value { get; set; }

        /// <summary>
        /// Set the number of outputs. The output to which to send the selected frame is based on the result of the evaluation. 
        /// 
        /// Default value is 1. 
        /// </summary>
        public int? Outputs { get; set; }

        public SelectFilter()
        {
            FilterName = "select";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(Value != null)
                paramList.Add(new CommandParam(null, Value));

            if(Outputs.HasValue)
                paramList.Add(new CommandParam("outputs", Outputs.Value.ToString()));

            return paramList;
        }
    }
}