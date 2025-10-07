
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class FilterGraphBuilder
    {
        public FilterGraph FilterGraph { get; private set; }

        public FilterGraphBuilder()
        {
            FilterGraph = new FilterGraph();
        }

        public FilterGraphBuilder(FilterGraph source)
        {
            FilterGraph = source;
        }

        public FilterChainBuilder BuildFilterChain()
        {
            FilterChain chain = new FilterChain();
            FilterGraph.FilterChains.Add(chain);

            return new FilterChainBuilder(chain);
        }

        /// <summary>
        /// Overlay watermark over video
        /// </summary>
        public FilterGraphBuilder AddWatermark(
            WatermarkParameters watermarkParams, 
            int vidInputIndex, 
            int imgInputIndex,
            string outputLink = null,
            bool isAnimated = false)
        {
            string scaleX;
            string scaleY;
            double fadeTime = watermarkParams.FadeTimeMilliseconds / 1000.0;
            int inDelay = watermarkParams.InDelay;
            int outDelay = watermarkParams.OutDelay + inDelay;

            FilterChainBuilder vidChainBuilder = BuildFilterChain();
            vidChainBuilder
            .AddInputLink(vidInputIndex)
            .AddInputLink(imgInputIndex);

            FilterChainBuilder watermarkChainBuilder = BuildFilterChain()
            .AddInputLink(imgInputIndex);

            if(outputLink != null)
                watermarkChainBuilder.AddOutputLink(outputLink);

            switch(watermarkParams.Position)
            {
                case WatermarkParameters.AlignmentPositions.N:
                    scaleX = "(main_w-overlay_w)/2";
                    scaleY = "(overlay_h)/2";
                break;
                case WatermarkParameters.AlignmentPositions.NE:
                    scaleX = "(main_w-overlay_w)";
                    scaleY = "(overlay_h)/2";
                break;
                case WatermarkParameters.AlignmentPositions.E:
                    scaleX = "(main_w-overlay_w)";
                    scaleY = "(main_h-overlay_h)/2";
                break;
                case WatermarkParameters.AlignmentPositions.SE:
                    scaleX = "(main_w-overlay_w)";
                    scaleY = "(main_h-overlay_h)";
                break;
                case WatermarkParameters.AlignmentPositions.S:
                    scaleX = "(main_w-overlay_w)/2";
                    scaleY = "(main_h-overlay_h)";
                break;
                case WatermarkParameters.AlignmentPositions.SW:
                    scaleX = "0";
                    scaleY = "(main_h-overlay_h)";
                break;
                case WatermarkParameters.AlignmentPositions.W:
                    scaleX = "0";
                    scaleY = "(main_h-overlay_h)/2";
                break;
                case WatermarkParameters.AlignmentPositions.NW:
                    scaleX = "0";
                    scaleY = "(overlay_h)/2";
                break;
                default:
                    scaleX = "(main_w-overlay_w)/2";
                    scaleY = "(main_h-overlay_h)/2";
                break;
            }

            if(!isAnimated)
            {
                vidChainBuilder.AddOverlay(scaleX, scaleY);

                watermarkChainBuilder.AddTrim("0", "30");

                if(watermarkParams.InDelay > 0)
                {
                    watermarkChainBuilder.AddFade(inDelay, fadeTime, FadeType.In);
                }

                if(watermarkParams.OutDelay > 0)
                {
                    watermarkChainBuilder.AddFade(outDelay, fadeTime, FadeType.Out);
                }

                watermarkChainBuilder.AddLoop(999, 750, 0).AddSetPTS("N/25/TB");
            }
            else
            {
                //Gif's alpha does not support fade so ignore
                vidChainBuilder.AddOverlay(scaleX, scaleY, true);

                watermarkChainBuilder.AddFormat("yuva444p").AddLoop(999, 750, 0);
            }

            return this;
        }
    }
}