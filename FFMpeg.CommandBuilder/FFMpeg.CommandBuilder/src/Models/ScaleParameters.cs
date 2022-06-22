
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public enum FFMpegScalingMode
    {
        Source = 0,
        Width = 1,
        Height = 2,
        Exact = 3,
        FitFill = 4,
        Fit = 5
    }

    public class ScaleParameters
    {
        public FFMpegScalingMode ScalingMode { get; set; }

        public VideoDimensions ScaleSize { get; set; }
        
        public bool Upscale { get; set; }

        public string FillColor { get; set; }
    }
}
