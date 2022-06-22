using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Add paddings to the input image, and place the original input at the provided x, y coordinates.
    /// 
    /// https://ffmpeg.org/ffmpeg-filters.html#toc-pad-1
    /// </summary>
    public class PadFilter : FilterBase
    {
        public string Width { get; set; }

        public string Height { get; set; }

        public string X { get; set; }

        public string Y { get; set; }

        /// <summary>
        /// Specify the color of the padded area. https://ffmpeg.org/ffmpeg-utils.html#color-syntax
        /// 
        /// The default value of color is "black".
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Set when the expressions for x, and y are evaluated. It accepts the following values:
        /// ‘init’: only evaluate expressions once during the filter initialization or when a command is processed
        /// ‘frame’: evaluate expressions for each incoming frame 
        /// 
        /// Default value is ‘frame’. 
        /// </summary>
        public CoordinateEval? Eval { get; set; }

        /// <summary>
        /// Pad to aspect instead to a resolution.
        /// </summary>
        public string Aspect { get; set; }

        public PadFilter()
        {
            FilterName = "pad";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(Width != null)
                paramList.Add(new CommandParam("w", Width));

            if(Height != null)
                paramList.Add(new CommandParam("h", Height));

            if(X != null)
                paramList.Add(new CommandParam("x", X));

            if(Y != null)
                paramList.Add(new CommandParam("y", Y));

            if(Eval.HasValue)
                paramList.Add(new CommandParam("eval", Eval.Value.ToString().ToLower()));

            if(Color != null)
                paramList.Add(new CommandParam("color", Color));

            if(Aspect != null)
                paramList.Add(new CommandParam("aspect", Aspect));

            return paramList;
        }
    }
}