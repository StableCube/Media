using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.WebP
{
    public class Gif2WebPOptions
    {
        private string OutputFile { get; set; }
        private bool? Lossy { get; set; }
        private bool? Mixed { get; set; }
        private float? Quality { get; set; }
        private bool? MinSize { get; set; }
        private int? CompressionMethod { get; set; }
        private Gif2WebPKMinMax? FrameDistance { get; set; }
        private string Metadata { get; set; }
        private int? DeblockingStrength { get; set; }
        private bool? UseMultithreading { get; set; }
        private bool? LoopCompatibility { get; set; }
        private bool? Verbose { get; set; }
        private bool? Quiet { get; set; }

        /// <summary>
        /// Specify the name of the output WebP file. If omitted, gif2webp will perform 
        /// conversion but only report statistics. Using "-" as output name will direct 
        /// output to 'stdout'.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithOutput(string value)
        {
            OutputFile = value;

            return this;
        }

        /// <summary>
        /// Encode the image using lossy compression.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithLossy(bool value)
        {
            Lossy = value;

            return this;
        }

        /// <summary>
        /// Mixed compression mode: optimize compression of the image by picking either lossy 
        /// or lossless compression for each frame heuristically.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithMixed(bool value)
        {
            Mixed = value;

            return this;
        }

        /// <summary>
        /// Specify the compression factor for RGB channels between 0 and 100. 
        /// The default is 75. In case of lossless compression (default), a small factor enables 
        /// faster compression speed, but produces a larger file. Maximum compression is achieved 
        /// by using a value of 100. In case of lossy compression (specified by the -lossy option), 
        /// a small factor produces a smaller file with lower quality. Best quality is achieved by 
        /// using a value of 100.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithQuality(float value)
        {
            Quality = Clamp(value, 0, 100);

            return this;
        }

        /// <summary>
        /// Encode image to achieve smallest size. This disables key frame insertion and picks 
        /// the dispose method resulting in the smallest output for each frame. It uses lossless 
        /// compression by default, but can be combined with -q, -m, -lossy or -mixed options.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithMinSize(bool value)
        {
            MinSize = value;

            return this;
        }

        /// <summary>
        /// Specify the compression method to use. This parameter controls the trade off between 
        /// encoding speed and the compressed file size and quality. Possible values range from 0 to 6. 
        /// Default value is 4. When higher values are used, the encoder will spend more time inspecting 
        /// additional encoding possibilities and decide on the quality gain. Lower value can result in 
        /// faster processing time at the expense of larger file size and lower compression quality.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithCompressionMethod(int value)
        {
            CompressionMethod = Clamp(value, 0, 6);

            return this;
        }

        /// <summary>
        /// Specify the minimum and maximum distance between consecutive key frames (independently decodable frames) 
        /// in the output animation. The tool will insert some key frames into the output animation as 
        /// needed so that this criteria is satisfied.
        /// 
        /// A kmax value of 0 will turn off insertion of key frames. A kmax value of 1 will result in all frames 
        /// being key frames. kmin value is not taken into account in both these special cases. Typical values 
        /// are in the range 3 to 30. Default values are kmin = 9, kmax = 17 for lossless compression and 
        /// kmin = 3, kmax = 5 for lossy compression.
        ///
        /// These two options are relevant only for animated images with large number of frames (>50).
        /// 
        /// When lower values are used, more frames will be converted to key frames. This may lead to 
        /// smaller number of frames required to decode a frame on average, thereby improving the decoding 
        /// performance. But this may lead to slightly bigger file sizes. Higher values may lead to 
        /// worse decoding performance, but smaller file sizes.
        /// 
        /// Some restrictions:
        /// kmin < kmax,
        /// kmin >= kmax / 2 + 1; and
        /// kmax - kmin <= 30.
        /// 
        /// If any of these restrictions are not met, they will be enforced automatically.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithFrameDistance(Gif2WebPKMinMax value)
        {
            FrameDistance = value;

            return this;
        }

        /// <summary>
        /// A comma separated list of metadata to copy from the input to the output if present. 
        /// Valid values: all, none, icc, xmp. The default is xmp.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithMetadata(string value)
        {
            Metadata = value;

            return this;
        }

        /// <summary>
        /// For lossy encoding only (specified by the -lossy option). 
        /// Specify the strength of the deblocking filter, between 0 (no filtering) and 100 (maximum filtering). 
        /// A value of 0 will turn off any filtering. Higher value will increase the strength of 
        /// the filtering process applied after decoding the picture. The higher the value the smoother 
        /// the picture will appear. Typical values are usually in the range of 20 to 50.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithDeblockingStrength(int value)
        {
            DeblockingStrength = Clamp(value, 0, 100);

            return this;
        }

        /// <summary>
        /// Use multi-threading for encoding, if possible.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithUseMultithreading(bool value)
        {
            UseMultithreading = value;

            return this;
        }

        /// <summary>
        /// If enabled, handle the loop information in a compatible fashion for 
        /// Chrome version prior to M62 (inclusive) and Firefox.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithLoopCompatibility(bool value)
        {
            LoopCompatibility = value;

            return this;
        }

        /// <summary>
        /// Print extra information.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithVerbose(bool value)
        {
            Verbose = value;

            return this;
        }

        /// <summary>
        /// Do not print anything.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Gif2WebPOptions WithQuiet(bool value)
        {
            Quiet = value;

            return this;
        }

        public Dictionary<string, string> GetCommandArguments()
        {
            var args = new Dictionary<string, string>();

            if(!string.IsNullOrEmpty(OutputFile))
                args.Add("-o", OutputFile);

            if(Lossy.HasValue && Lossy.Value)
                args.Add("-lossy", null);

            if(Mixed.HasValue && Mixed.Value)
                args.Add("-mixed", null);

            if(Quality.HasValue)
                args.Add("-q", Quality.Value.ToString());

            if(MinSize.HasValue && MinSize.Value)
                args.Add("-min_size", null);

            if(CompressionMethod.HasValue)
                args.Add("-m", CompressionMethod.Value.ToString());

            if(FrameDistance.HasValue)
            {
                args.Add("-kmin", FrameDistance.Value.Min.ToString());
                args.Add("-kmax", FrameDistance.Value.Max.ToString());
            }
            
            if(!string.IsNullOrEmpty(Metadata))
                args.Add("-metadata", Metadata);

            if(DeblockingStrength.HasValue)
                args.Add("-f", DeblockingStrength.Value.ToString());

            if(UseMultithreading.HasValue && UseMultithreading.Value)
                args.Add("-mt", null);

            if(LoopCompatibility.HasValue && LoopCompatibility.Value)
                args.Add("-loop_compatibility", null);

            if(Verbose.HasValue && Verbose.Value)
                args.Add("-v", null);

            if(Quiet.HasValue && Quiet.Value)
                args.Add("-quiet", null);

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
