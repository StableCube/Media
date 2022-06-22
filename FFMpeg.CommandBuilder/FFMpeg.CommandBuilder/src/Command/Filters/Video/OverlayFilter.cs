using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public enum FormatOption
    {
        Yuv420,
        Yuv422,
        Yuv444,
        RGB,
        GBRP,
        Auto,
    }

    public enum AlphaOption
    {
        PreMultiplied,
        Straight,
    }

    /// <summary>
    /// Overlay one video on top of another.
    /// It takes two inputs and has one output. The first input is the "main" video on which the second input is overlaid. 
    /// </summary>
    public class OverlayFilter : FilterBase
    {
        public string X { get; set; }

        public string Y { get; set; }

        public MultiInputFilterOptions MultiInputOptions { get; set; }

        /// <summary>
        /// Set when the expressions for x, and y are evaluated. It accepts the following values:
        /// ‘init’: only evaluate expressions once during the filter initialization or when a command is processed
        /// ‘frame’: evaluate expressions for each incoming frame 
        /// 
        /// Default value is ‘frame’. 
        /// </summary>
        public CoordinateEval? Eval { get; set; }

        /// <summary>
        /// Set the format for the output video. It accepts the following values:
        /// ‘yuv420’: force YUV420 output
        /// ‘yuv422’: force YUV422 output
        /// ‘yuv444’: force YUV444 output
        /// ‘rgb’: force packed RGB output
        /// ‘gbrp’: force planar RGB output
        /// ‘auto’: automatically pick format
        /// 
        /// Default value is ‘yuv420’.
        /// </summary>
        public FormatOption? Format { get; set; }

        /// <summary>
        /// Set format of alpha of the overlaid video, it can be straight or premultiplied. Default is straight. 
        /// </summary>
        public AlphaOption? Alpha { get; set; }

        public OverlayFilter()
        {
            FilterName = "overlay";
        }

        public override List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(X != null)
                paramList.Add(new CommandParam("x", X));

            if(Y != null)
                paramList.Add(new CommandParam("y", Y));

            if(MultiInputOptions != null)
                paramList.AddRange(MultiInputOptions.GetFilterParams());
            
            if(Eval.HasValue)
                paramList.Add(new CommandParam("eval", Eval.Value.ToString().ToLower()));

            if(Alpha.HasValue)
                paramList.Add(new CommandParam("alpha", Alpha.Value.ToString().ToLower()));

            return paramList;
        }
    }
}