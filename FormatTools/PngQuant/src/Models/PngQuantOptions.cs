using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.PngQuant
{
    public class PngQuantOptions
    {
        public bool? Force { get; private set; }
        public bool? SkipIfLarger { get; private set; }
        public string OutputFile { get; private set; }
        public string Extention { get; private set; }
        public PngQuantQuality? Quality { get; private set; }
        public int? Speed { get; private set; }
        public bool? Verbose { get; private set; }
        public bool? Strip { get; private set; }
        public bool? NoFloydSteinberg { get; private set; }

        /// <summary>
        /// overwrite existing output files
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PngQuantOptions WithForce(bool value)
        {
            Force = value;

            return this;
        }

        /// <summary>
        /// only save converted files if they're smaller than original
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PngQuantOptions WithSkipIfLarger(bool value)
        {
            SkipIfLarger = value;

            return this;
        }

        /// <summary>
        /// destination file path to use instead of
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PngQuantOptions WithOutputFile(string value)
        {
            OutputFile = value;

            return this;
        }

        /// <summary>
        /// set custom suffix/extension for output filenames
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PngQuantOptions WithExtention(string value)
        {
            Extention = value;

            return this;
        }

        /// <summary>
        /// Instructs pngquant to use the least amount of colors required to meet or exceed the max quality. 
        /// If conversion results in quality below the min quality the image won't be saved (if outputting to 
        /// stdout, 24-bit original will be output) and pngquant will exit with status code 99.
        /// 
        /// min and max are numbers in range 0 (worst) to 100 (perfect), similar to JPEG. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PngQuantOptions WithQuality(PngQuantQuality value)
        {
            Quality = value;

            return this;
        }

        /// <summary>
        /// Speed/quality trade-off from 1 (brute-force) to 10 (fastest). The default is 3. 
        /// Speed 10 has 5% lower quality, but is 8 times faster than the default.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PngQuantOptions WithSpeed(int value)
        {
            Speed = Clamp(value, 1, 10);

            return this;
        }

        /// <summary>
        /// print status messages
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PngQuantOptions WithVerbose(bool value)
        {
            Verbose = value;

            return this;
        }

        /// <summary>
        /// remove optional metadata (default on Mac)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PngQuantOptions WithStrip(bool value)
        {
            Strip = value;

            return this;
        }

        /// <summary>
        /// disable Floyd-Steinberg dithering
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PngQuantOptions WithNoFloydSteinberg(bool value)
        {
            NoFloydSteinberg = value;

            return this;
        }

        public Dictionary<string, string> GetCommandArguments()
        {
            var args = new Dictionary<string, string>();

            if(Force.HasValue && Force.Value == true)
                args.Add("--force", null);

            if(SkipIfLarger.HasValue && SkipIfLarger.Value == true)
                args.Add("--skip-if-larger", null);

            if(!string.IsNullOrEmpty(OutputFile))
                args.Add("--output", OutputFile);

            if(!string.IsNullOrEmpty(Extention))
                args.Add("--ext", Extention);

            if(Quality != null)
                args.Add("--quality", $"{Quality.Value.Min}-{Quality.Value.Max}");

            if(Speed.HasValue)
                args.Add("--speed", Speed.Value.ToString());

            if(Verbose.HasValue && Verbose.Value == true)
                args.Add("--verbose", null);

            if(Strip.HasValue && Strip.Value == true)
                args.Add("--strip", null);

            if(NoFloydSteinberg.HasValue && NoFloydSteinberg.Value == true)
                args.Add("--nofs", null);

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
    }
}
