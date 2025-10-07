using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.WebP
{
    public class CWebPOptions
    {
        private string OutputFile { get; set; }
        private bool? Lossless { get; set; }
        private int? NearLossless { get; set; }
        private float? Quality { get; set; }
        private int? LosslessQuality { get; set; }
        private int? AlphaQuality { get; set; }
        private CWebPPreset? Preset { get; set; }
        private int? CompressionMethod { get; set; }
        private CWebPCrop? Crop { get; set; }
        private CWebPResizeDimensions? Resize { get; set; }
        private bool? UseMultithreading { get; set; }
        private bool? LowMemory { get; set; }
        private bool? Exact { get; set; }
        private CWebPLossyOptions LossyOptions { get; set; }
        private CWebPAdvancedOptions AdvancedOptions { get; set; }
        private CWebPLoggingOptions LoggingOptions { get; set; }

        /// <summary>
        /// Specify the name of the output WebP file. If omitted, cwebp will perform compression but only 
        /// report statistics. Using "-" as output name will direct output to 'stdout'.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPOptions WithOutput(string value)
        {
            OutputFile = value;

            return this;
        }

        /// <summary>
        /// Encode the image without any loss. For images with fully transparent area, the invisible 
        /// pixel values (R/G/B or Y/U/V) will be preserved only if the -exact option is used.
        /// </summary>
        /// <param name="value">between 0 and 100</param>
        /// <returns></returns>
        public CWebPOptions WithLossless(bool value)
        {
            Lossless = value;

            return this;
        }

        /// <summary>
        /// Specify the level of near-lossless image preprocessing. This option adjusts pixel values to help 
        /// compressibility, but has minimal impact on the visual quality. It triggers lossless compression 
        /// mode automatically. The range is 0 (maximum preprocessing) to 100 (no preprocessing, the default). 
        /// The typical value is around 60. Note that lossy with -q 100 can at times yield better results.
        /// </summary>
        /// <param name="value">between 0 and 100</param>
        /// <returns></returns>
        public CWebPOptions WithNearLossless(int value)
        {
            NearLossless = Clamp(value, 0, 100);

            return this;
        }

        /// <summary>
        /// Specify the compression factor for RGB channels between 0 and 100. The default is 75.
        /// 
        /// In case of lossy compression (default), a small factor produces a smaller file with lower quality. 
        /// Best quality is achieved by using a value of 100.
        /// 
        /// In case of lossless compression (specified by the -lossless option), a small factor enables 
        /// faster compression speed, but produces a larger file. Maximum compression is achieved by using a value of 100.
        /// </summary>
        /// <param name="value">between 0 and 100</param>
        /// <returns></returns>
        public CWebPOptions WithQuality(float value)
        {
            Quality = Clamp(value, 0, 100);

            return this;
        }

        /// <summary>
        /// Switch on lossless compression mode with the specified level between 0 and 9, with level 0 
        /// being the fastest, 9 being the slowest. Fast mode produces larger file size than slower ones. 
        /// A good default is -z 6. This option is actually a shortcut for some predefined settings for 
        /// quality and method. If options -q or -m are subsequently used, they will 
        /// invalidate the effect of this option.
        /// </summary>
        /// <param name="value">between 0 and 9</param>
        /// <returns></returns>
        public CWebPOptions WithLosslessQuality(int value)
        {
            LosslessQuality = Clamp(value, 0, 9);

            return this;
        }

        /// <summary>
        /// Specify the compression factor for alpha compression between 0 and 100. 
        /// Lossless compression of alpha is achieved using a value of 100, while the lower values 
        /// result in a lossy compression. The default is 100.
        /// </summary>
        /// <param name="value">between 0 and 100</param>
        /// <returns></returns>
        public CWebPOptions WithAlphaQuality(int value)
        {
            AlphaQuality = Clamp(value, 0, 100);

            return this;
        }

        /// <summary>
        /// Specify a set of pre-defined parameters to suit a particular type of source material. 
        /// Possible values are: default, photo, picture, drawing, icon, text.
        ///
        /// Since -preset overwrites the other parameters' values (except the -q one), this option 
        /// should preferably appear first in the order of the arguments.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPOptions WithPreset(CWebPPreset value)
        {
            Preset = value;

            return this;
        }

        /// <summary>
        /// Specify the compression method to use. This parameter controls the trade off between encoding 
        /// speed and the compressed file size and quality. Possible values range from 0 to 6. Default value is 4. 
        /// When higher values are used, the encoder will spend more time inspecting additional encoding 
        /// possibilities and decide on the quality gain. Lower value can result in faster processing time 
        /// at the expense of larger file size and lower compression quality.
        /// </summary>
        /// <param name="value">0 to 6</param>
        /// <returns></returns>/
        public CWebPOptions WithCompressionMethod(int value)
        {
            CompressionMethod = Clamp(value, 0, 6);

            return this;
        }

        /// <summary>
        /// Crop the source to a rectangle with top-left corner at coordinates (x_position, y_position) and size width x height. 
        /// This cropping area must be fully contained within the source rectangle. 
        /// Note: the cropping is applied before any scaling.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPOptions WithCrop(CWebPCrop value)
        {
            Crop = value;

            return this;
        }

        /// <summary>
        /// Crop the source to a rectangle with top-left corner at coordinates (x_position, y_position) and 
        /// size width x height. This cropping area must be fully contained within the source rectangle. 
        /// Note: the cropping is applied before any scaling.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPOptions WithResize(CWebPResizeDimensions value)
        {
            Resize = value;

            return this;
        }

        /// <summary>
        /// Use multi-threading for encoding, if possible.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPOptions WithUseMultithreading(bool value)
        {
            UseMultithreading = value;

            return this;
        }

        /// <summary>
        /// Reduce memory usage of lossy encoding by saving four times the compressed size (typically). 
        /// This will make the encoding slower and the output slightly different in size and distortion. 
        /// This flag is only effective for methods 3 and up, and is off by default. 
        /// Note that leaving this flag off will have some side effects on the bitstream: it forces 
        /// certain bitstream features like number of partitions (forced to 1). 
        /// Note that a more detailed report of bitstream size is printed by cwebp when using this option.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPOptions WithLowMemory(bool value)
        {
            LowMemory = value;

            return this;
        }

        /// <summary>
        /// Preserve RGB values in transparent area. The default is off, to help compressibility.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPOptions WithExact(bool value)
        {
            Exact = value;

            return this;
        }

        /// <summary>
        /// These options are only effective when doing lossy encoding (the default, with or without alpha).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPOptions WithLossyOptions(CWebPLossyOptions value)
        {
            LossyOptions = value;

            return this;
        }

        public CWebPOptions WithAdvancedOptions(CWebPAdvancedOptions value)
        {
            AdvancedOptions = value;

            return this;
        }

        public CWebPOptions WithLoggingOptions(CWebPLoggingOptions value)
        {
            LoggingOptions = value;

            return this;
        }

        public Dictionary<string, string> GetCommandArguments()
        {
            var args = new Dictionary<string, string>();

            if(OutputFile != null)
                args.Add("-o", OutputFile);

            if(Preset.HasValue)
                args.Add("-preset", AlphaQuality.Value.ToString().ToLower());

            if(Lossless.HasValue && Lossless.Value == true)
                args.Add("-lossless", null);

            if(NearLossless.HasValue)
                args.Add("-near_lossless", NearLossless.Value.ToString());

            if(Quality.HasValue)
                args.Add("-q", Quality.Value.ToString());

            if(LosslessQuality.HasValue)
                args.Add("-z", LosslessQuality.Value.ToString());

            if(AlphaQuality.HasValue)
                args.Add("-alpha_q", AlphaQuality.Value.ToString());

            if(CompressionMethod.HasValue)
                args.Add("-m", CompressionMethod.Value.ToString());

            if(Crop.HasValue)
                args.Add("-crop", $"{Crop.Value.X.ToString()} {Crop.Value.Y.ToString()} {Crop.Value.Width.ToString()} {Crop.Value.Height.ToString()}");

            if(Resize.HasValue)
                args.Add("-resize", $"{Resize.Value.Width.ToString()} {Resize.Value.Height.ToString()}");

            if(UseMultithreading.HasValue && UseMultithreading.Value == true)
                args.Add("-mt", null);

            if(LowMemory.HasValue && LowMemory.Value == true)
                args.Add("-low_memory", null);

            if(Exact.HasValue && Exact.Value == true)
                args.Add("-exact", null);

            if(AdvancedOptions != null)
            {
                foreach (var item in AdvancedOptions.GetCommandArguments())
                    args.Add(item.Key, item.Value);
            }
            
            if(LoggingOptions != null)
            {
                foreach (var item in LoggingOptions.GetCommandArguments())
                    args.Add(item.Key, item.Value);
            }

            return args;
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
    }
}
