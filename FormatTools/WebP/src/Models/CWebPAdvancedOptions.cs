using System;
using System.Collections.Generic;

namespace StableCube.Media.WebP
{
    public class CWebPAdvancedOptions
    {
        private int? DeblockingStrength { get; set; }
        private int? Sharpness { get; set; }
        private bool? StrongFiltering { get; set; }
        private bool? NoStrongFiltering { get; set; }
        private bool? SharpYUV { get; set; }
        private int? Sns { get; set; }
        private int? Segments { get; set; }
        private int? PartitionLimit { get; set; }

        /// <summary>
        /// Specify the strength of the deblocking filter, between 0 (no filtering) and 100 (maximum filtering). 
        /// A value of 0 will turn off any filtering. Higher value will increase the strength of 
        /// the filtering process applied after decoding the picture. The higher the value the 
        /// smoother the picture will appear. Typical values are usually in the range of 20 to 50.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPAdvancedOptions WithDeblockingStrength(int value)
        {
            DeblockingStrength = value;

            return this;
        }

        /// <summary>
        /// Specify the sharpness of the filtering (if used). Range is 0 (sharpest) to 7 (least sharp). Default is 0.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPAdvancedOptions WithSharpness(int value)
        {
            Sharpness = Clamp(value, 0, 7);

            return this;
        }

        /// <summary>
        /// Use strong filtering (if filtering is being used thanks to the -f option). Strong filtering is on by default.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPAdvancedOptions WithStrongFiltering(bool value)
        {
            StrongFiltering = value;

            return this;
        }

        /// <summary>
        /// Disable strong filtering (if filtering is being used thanks to the -f option) and use simple filtering instead.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPAdvancedOptions WithNoStrongFiltering(bool value)
        {
            NoStrongFiltering = value;

            return this;
        }

        /// <summary>
        /// Use more accurate and sharper RGB->YUV conversion if needed. Note that this process is slower 
        /// than the default 'fast' RGB->YUV conversion.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPAdvancedOptions WithSharpYUV(bool value)
        {
            SharpYUV = value;

            return this;
        }

        /// <summary>
        /// Specify the amplitude of the spatial noise shaping. Spatial noise shaping 
        /// (or sns for short) refers to a general collection of built-in algorithms used 
        /// to decide which area of the picture should use relatively less bits, and where 
        /// else to better transfer these bits. The possible range goes 
        /// from 0 (algorithm is off) to 100 (the maximal effect). The default value is 50.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPAdvancedOptions WithSns(int value)
        {
            Sns = Clamp(value, 0, 100);

            return this;
        }

        /// <summary>
        /// Change the number of partitions to use during the segmentation of the sns algorithm. 
        /// Segments should be in range 1 to 4. Default value is 4. This option has no effect for 
        /// methods 3 and up, unless -low_memory is used.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPAdvancedOptions WithSegments(int value)
        {
            Segments = Clamp(value, 0, 4);

            return this;
        }

        /// <summary>
        /// Degrade quality by limiting the number of bits used by some macroblocks. 
        /// Range is 0 (no degradation, the default) to 100 (full degradation). 
        /// Useful values are usually around 30-70 for moderately large images. 
        /// In the VP8 format, the so-called control partition has a limit of 512k and is used to store 
        /// the following information: whether the macroblock is skipped, which segment it belongs to, 
        /// whether it is coded as intra 4x4 or intra 16x16 mode, and finally the prediction modes 
        /// to use for each of the sub-blocks. For a very large image, 512k only leaves room to 
        /// few bits per 16x16 macroblock. The absolute minimum is 4 bits per macroblock. 
        /// Skip, segment, and mode information can use up almost all these 4 bits 
        /// (although the case is unlikely), which is problematic for very large images. 
        /// The partition_limit factor controls how frequently the most bit-costly mode (intra 4x4) will be used. 
        /// This is useful in case the 512k limit is reached and the following message is displayed: 
        /// Error code: 6 (PARTITION0_OVERFLOW: Partition #0 is too big to fit 512k). 
        /// If using -partition_limit is not enough to meet the 512k constraint, one should use 
        /// less segments in order to save more header bits per macroblock. See the -segments option.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPAdvancedOptions WithPartitionLimit(int value)
        {
            PartitionLimit = Clamp(value, 0, 100);

            return this;
        }

        public Dictionary<string, string> GetCommandArguments()
        {
            var args = new Dictionary<string, string>();

            if(DeblockingStrength.HasValue)
                args.Add("-f", DeblockingStrength.Value.ToString());

            if(DeblockingStrength.HasValue)
                args.Add("-sharpness", Sharpness.Value.ToString());

            if(StrongFiltering.HasValue && StrongFiltering.Value == true)
                args.Add("-strong", null);

            if(NoStrongFiltering.HasValue && NoStrongFiltering.Value == true)
                args.Add("-nostrong", null);

            if(SharpYUV.HasValue && SharpYUV.Value == true)
                args.Add("-sharp_yuv", null);

            if(Sns.HasValue)
                args.Add("-sns", Sns.Value.ToString());

            if(Segments.HasValue)
                args.Add("-segments", Segments.Value.ToString());

            if(PartitionLimit.HasValue)
                args.Add("-partition_limit", PartitionLimit.Value.ToString());

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
