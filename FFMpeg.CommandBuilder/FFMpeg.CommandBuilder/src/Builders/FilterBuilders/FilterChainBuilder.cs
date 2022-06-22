using System;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class FilterChainBuilder
    {
        public FilterChain FilterChain { get; private set; }

        public FilterChainBuilder()
        {
            FilterChain = new FilterChain();
        }

        public FilterChainBuilder(FilterChain source )
        {
            FilterChain = source;
        }

        public FilterChainBuilder AddInputLink(int fileInputIndex)
        {
            FilterChain.AddInputLink(fileInputIndex.ToString());

            return this;
        }

        public FilterChainBuilder AddInputLink(int fileInputIndex, int streamIndex)
        {
            FilterChain.AddInputLink($"{fileInputIndex.ToString()}:{streamIndex.ToString()}");

            return this;
        }

        public FilterChainBuilder AddInputLink(int fileInputIndex, char streamType)
        {
            FilterChain.AddInputLink($"{fileInputIndex.ToString()}:{streamType}");

            return this;
        }


        public FilterChainBuilder AddInputLink(StreamSpecifier stream)
        {
            FilterChain.AddInputLink(stream);

            return this;
        }

        public FilterChainBuilder AddInputLink(string linkName)
        {
            FilterChain.AddInputLink(linkName);

            return this;
        }

        public FilterChainBuilder AddOutputLink(string linkName)
        {
            FilterChain.AddOutputLink(linkName);
            
            return this;
        }

        public FilterChainBuilder AddOverlay(string x, string y, bool shortest = true)
        {
            OverlayFilter filter = new OverlayFilter()
            {
                X = x,
                Y = y,
                MultiInputOptions = new MultiInputFilterOptions()
                {
                    Shortest = shortest
                }
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        public FilterChainBuilder AddTrim(string start, string end)
        {
            TrimFilter filter = new TrimFilter()
            {
                Start = start,
                End = end
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        public FilterChainBuilder AddFade(int startTime, double duration, FadeType fadeType = FadeType.In, bool fadeOnlyAlpha = true)
        {
            FadeFilter filter = new FadeFilter()
            {
                Type = fadeType,
                StartTime = startTime,
                Duration = duration,
                Alpha = fadeOnlyAlpha
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        public FilterChainBuilder AddLoop(int loop, int size, int start)
        {
            LoopFilter filter = new LoopFilter()
            {
                Loop = loop,
                Size = size,
                Start = start,
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        public FilterChainBuilder AddSetPTS(string expression)
        {
            SetPTSFilter filter = new SetPTSFilter()
            {
                Value = expression,
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        public FilterChainBuilder AddFormat(string pixelFormats)
        {
            FormatFilter filter = new FormatFilter()
            {
                PixelFormats = pixelFormats,
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        public FilterChainBuilder AddPad(
            string width, 
            string height, 
            string x, 
            string y, 
            string color,
            string aspect = null
        )
        {
            PadFilter filter = new PadFilter()
            {
                Width = width,
                Height = height,
                X = x,
                Y = y,
                Color = color,
                Aspect = aspect
            };

            FilterChain.AddFilter(filter);

            return this;
        }
        
        public FilterChainBuilder AddScale(string width, string height, ForceOriginalAspectRatio mode = ForceOriginalAspectRatio.Decrease)
        {
            ScaleFilter filter = new ScaleFilter()
            {
                Width = width,
                Height = height,
                ForceOriginalAspectRatio = mode
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        public FilterChainBuilder AddFPS(
            double? fps = null, 
            double? startTime = null, 
            FPSTimestampRounding? rounding = null,
            FPSEndOfFileAction? endOfFileAction = null)
        {
            FPSFilter filter = new FPSFilter()
            {
                FPS = fps,
                StartTime = startTime,
                Round = rounding,
                EndOfFileAction = endOfFileAction
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        public FilterChainBuilder AddSelect(string expression)
        {
            SelectFilter filter = new SelectFilter()
            {
                Value = expression
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        public FilterChainBuilder AddThumbnail(int? batchSize = null)
        {
            ThumbnailFilter filter = new ThumbnailFilter()
            {
                BatchSize = batchSize
            };

            FilterChain.AddFilter(filter);

            return this;
        }

        /// <summary>
        /// Scale dynamically based upon mode
        /// </summary>
        public FilterChainBuilder AddDynamicScale(ScaleParameters scaleParams)
        {
            var scalingMode = scaleParams.ScalingMode;
            var targetWidth = ((int)Math.Round(scaleParams.ScaleSize.Width / 2.0)) * 2;
            var targetHeight = ((int)Math.Round(scaleParams.ScaleSize.Height / 2.0)) * 2;
            string targetWidthStr = targetWidth.ToString();
            string targetHeightStr = targetHeight.ToString();

            switch (scaleParams.ScalingMode)
            {
                case FFMpegScalingMode.Source:
                    AddScale("-2", "-2");
                break;
                case FFMpegScalingMode.Height:
                    if(scaleParams.Upscale)
                    {
                        AddScale("-2", targetHeightStr);
                    }
                    else
                    {
                        AddScale(
                            "-2",
                            $"if(gt(ih\\,{scaleParams.ScaleSize.Height})\\,{scaleParams.ScaleSize.Height}\\,ih)");
                    }
                    
                break;

                case FFMpegScalingMode.Width:
                    if(scaleParams.Upscale)
                    {
                        AddScale(targetWidthStr, "-2");
                    }
                    else
                    {
                        AddScale(
                            $"if(gt(iw\\,{scaleParams.ScaleSize.Width})\\,{scaleParams.ScaleSize.Width}\\,iw)",
                            "-2"
                        );
                    }
                    
                break;

                case FFMpegScalingMode.Exact:
                    AddScale(targetWidthStr, targetHeightStr, ForceOriginalAspectRatio.Disable);
                break;

                case FFMpegScalingMode.FitFill:
                    var hexColor = "000000";
                    if(scaleParams.FillColor != String.Empty)
                        hexColor = scaleParams.FillColor.Replace("#", String.Empty);

                    string scaleWidth = targetWidthStr;
                    string scaleHeight = targetHeightStr;
                    if(scaleParams.Upscale == false)
                    {
                        scaleWidth = $"if(gt(iw\\,{scaleWidth})\\,{scaleWidth}\\,iw)";
                        scaleHeight = $"if(gt(ih\\,{scaleHeight})\\,{scaleHeight}\\,ih)";
                    }

                    AddScale(scaleWidth, scaleHeight);

                    AddPad(targetWidthStr, targetHeightStr, "(ow-iw)/2", "(oh-ih)/2", hexColor);

                break;

                case FFMpegScalingMode.Fit:
                    string width = targetWidthStr;
                    string height = targetHeightStr;
                    if(scaleParams.Upscale == false)
                    {
                        width = $"if(gt(iw\\,{width})\\,{width}\\,iw)";
                        height = $"if(gt(ih\\,{height})\\,{height}\\,ih)";
                    }

                    AddScale(width, height);
                break;

                default:
                    throw new ArgumentOutOfRangeException("ScalingMode: " + scaleParams.ScalingMode);
            }

            return this;
        }
    }
}