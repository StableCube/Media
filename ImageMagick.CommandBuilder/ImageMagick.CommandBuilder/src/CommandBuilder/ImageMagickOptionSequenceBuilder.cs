
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    /// <summary>
    /// Core ImageMagick options
    /// </summary>
    public abstract class ImageMagickOptionSequenceBuilder
    {
        protected CommandOptionSequence _options;

        public ImageMagickOptionSequenceBuilder(CommandOptionSequence source)
        {
            _options = source;
		}

        public CommandOptionSequence Build()
        {
            return _options;
        }

        private static int Clamp(int value, int min, int max)
        {
            if(value < min)
                value = min;

            if(value > max)
                value = max;

            return value;
        }

        private static float Clamp(float value, float min, float max)
        {
            if(value < min)
                value = min;

            if(value > max)
                value = max;

            return value;
        }

        public ImageMagickOptionSequenceBuilder AddAlpha(string type)
        {
            var alphaOp = new AlphaOption()
            {
                Type = type
            };

            _options.Add(alphaOp);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddResize(int width, int height, bool upscale = false, bool ignoreAspectRatio = false)
        {
            var resizeOp = new ResizeOption()
            {
                Geometry = new ImageGeometry()
                {
                    Greater = (upscale == false),
                    Width = width,
                    Height = height,
                    IgnoreAspectRatio = ignoreAspectRatio
                }
            };

            _options.Add(resizeOp);

            return this;
        }

        /// <summary>
        /// Use this option to specify the width and height of raw images whose dimensions are unknown such as 
        /// GRAY, RGB, or CMYK. In addition to width and height, use -size with an offset to skip any header information 
        /// in the image or tell the number of colors in a MAP image file, (e.g. -size 640x512+256).
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="upscale"></param>
        /// <param name="fillColor"></param>
        /// <returns></returns>
        public ImageMagickOptionSequenceBuilder AddSize(int width, int height, bool upscale = false, ImageColor fillColor = null)
        {
            var option = new SizeOption()
            {
                Geometry = new ImageGeometry()
                {
                    Greater = (upscale == false),
                    Width = width,
                    Height = height,
                }
            };

            if(fillColor != null)
                option.Canvas = fillColor;

            _options.Add(option);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddGravity(GravityOption.GravityType gravityType)
        {
            var option = new GravityOption()
            {
                Gravity = gravityType
            };

            _options.Add(option);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddCoalesce()
        {
            var option = new CoalesceOption();

            _options.Add(option);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddComposite()
        {
            var option = new CompositeOption();

            _options.Add(option);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddSwap(int index1, int index2)
        {
            var option = new SwapOption() 
            {
                Index1 = index1,
                Index2 = index2
            };

            _options.Add(option);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddSwapAlt()
        {
            var option = new SwapOption() 
            {
                Alternate = true
            };

            _options.Add(option);

            return this;
        }

        /// <summary>
        /// Handle multiple images forming a set of image layers or animation frames.
        /// 
        /// compare-any: Crop the second and later frames to the smallest rectangle that contains all 
        /// the differences between the two images. No GIF -dispose methods are taken into account. 
        /// 
        /// compare-clear: As 'compare-any' but crop to the bounds of any opaque pixels which become 
        /// transparent in the second frame. That is the smallest image needed to mask or erase 
        /// pixels for the next frame. 
        /// 
        /// compare-overlay: As 'compare-any' but crop to pixels that add extra color to the next image, 
        /// as a result of overlaying color pixels. That is the smallest single overlaid 
        /// image to add or change colors. 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public ImageMagickOptionSequenceBuilder AddLayers(LayerMethod method)
        {
            var option = new LayersOption() 
            {
                Method = method
            };

            _options.Add(option);

            return this;
        }

        /// <summary>
        /// Set the width and height using the size portion of the geometry argument. 
        /// See Image Geometry for complete details about the geometry argument. 
        /// Offsets are ignored. 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public ImageMagickOptionSequenceBuilder AddBorder(int size)
        {
            var option = new BorderOption()
            {
                Size = size
            };

            _options.Add(option);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddBackground(ImageColor color)
        {
            var option = new BackgroundOption()
            {
                Color = color
            };

            _options.Add(option);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddBorderColor(ImageColor color)
        {
            var option = new BorderColorOption()
            {
                Color = color
            };

            _options.Add(option);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddDefine(string definition)
        {
            var option = new DefineOption()
            {
                Definition = definition
            };

            _options.Add(option);

            return this;
        }

        public ImageMagickOptionSequenceBuilder AddMonitor()
        {
            var option = new MonitorOption();

            _options.Add(option);

            return this;
        }

        /// <summary>
        /// JPEG/MIFF/PNG compression level.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ImageMagickOptionSequenceBuilder AddQuality(int value)
        {
            var option = new QualityOption()
            {
                Quality = Clamp(value, 1, 100)
            };

            _options.Add(option);

            return this;
        }

        /// <summary>
        /// If the image is enlarged, unfilled areas are set to the background color. 
        /// To position the image, use offsets in the geometry specification or precede with a -gravity setting. 
        /// To specify how to compose the image with the background, use -compose.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ImageMagickOptionSequenceBuilder AddExtent(ImageDimensions value)
        {
            var option = new ExtentOption()
            {
                Dimensions = value
            };

            _options.Add(option);

            return this;
        }

        /// <summary>
        /// Find areas that has changed between images 
        ///
        /// Given a sequence of images all the same size, such as produced by -coalesce, replace the 
        /// second and later images, with a smaller image of just the area that changed relative 
        /// to the previous image.
        /// 
        /// The resulting sequence of images can be used to optimize an animation sequence, though 
        /// will not work correctly for GIF animations when parts of the animation can go 
        /// from opaque to transparent.
        /// 
        /// This option is actually equivalent to the -layers method 'compare-any'. 
        /// </summary>
        /// <returns></returns>
        public ImageMagickOptionSequenceBuilder AddDeconstruct()
        {
            _options.Add(new DeconstructOption());

            return this;
        }
    }
}