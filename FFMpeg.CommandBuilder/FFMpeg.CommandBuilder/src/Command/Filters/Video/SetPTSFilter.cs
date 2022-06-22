using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Change the PTS (presentation timestamp) of the input frames. 
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#setpts_002c-asetpts
    ///  </summary>
    public class SetPTSFilter : FilterBase
    {
        public string Value { get; set; }

        public SetPTSFilter()
        {
            FilterName = "setpts";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(Value != null)
                paramList.Add(new CommandParam(null, Value));

            return paramList;
        }
    }
}