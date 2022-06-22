using System;

namespace StableCube.Media.WebP
{
    public class CWebPLossyOptions
    {
        private int? Size { get; set; }
        private float? Psnr { get; set; }
        private int? Pass { get; set; }
        private bool? AutoFilter { get; set; }
        private bool? JpegLike { get; set; }

        /// <summary>
        /// Specify a target size (in bytes) to try and reach for the compressed output. 
        /// The compressor will make several passes of partial encoding in order to get as close as 
        /// possible to this target. If both -size and -psnr are used, -size value will prevail.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLossyOptions WithSize(int value)
        {
            Size = value;

            return this;
        }

        /// <summary>
        /// Specify a target PSNR (in dB) to try and reach for the compressed output. 
        /// The compressor will make several passes of partial encoding in order to get as close as 
        /// possible to this target. If both -size and -psnr are used, -size value will prevail.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLossyOptions WithPsnr(float value)
        {
            Psnr = value;

            return this;
        }

        /// <summary>
        /// Set a maximum number of passes to use during the dichotomy used by options -size or -psnr. 
        /// Maximum value is 10, default is 1. If options -size or -psnr were used, but -pass wasn't 
        /// specified, a default value of '6' passes will be used.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLossyOptions WithPass(int value)
        {
            Pass = Clamp(value, 1, 10);

            return this;
        }

        /// <summary>
        /// Turns auto-filter on. This algorithm will spend additional 
        /// time optimizing the filtering strength to reach a well-balanced quality.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLossyOptions WithAutoFilter(bool value)
        {
            AutoFilter = value;

            return this;
        }

        /// <summary>
        /// Change the internal parameter mapping to better match the expected size of JPEG compression. 
        /// This flag will generally produce an output file of similar size to its JPEG 
        /// equivalent (for the same -q setting), but with less visual distortion.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLossyOptions WithJpegLike(bool value)
        {
            JpegLike = value;

            return this;
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
