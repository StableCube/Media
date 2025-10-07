using System;
using System.Collections.Generic;

namespace StableCube.Media.WebP
{
    public class CWebPLoggingOptions
    {
        private bool? Verbose { get; set; }
        private bool? PrintPsnr { get; set; }
        private bool? PrintSsim { get; set; }
        private bool? PrintLsim { get; set; }
        private bool? Progress { get; set; }
        private bool? Quiet { get; set; }
        private bool? Short { get; set; }
        private int? Map { get; set; }

        /// <summary>
        /// Print extra information (encoding time in particular).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLoggingOptions WithVerbose(bool value)
        {
            Verbose = value;

            return this;
        }

        /// <summary>
        /// Compute and report average PSNR (Peak-Signal-To-Noise ratio).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLoggingOptions WithPrintPsnr(bool value)
        {
            PrintPsnr = value;

            return this;
        }

        /// <summary>
        /// Compute and report average SSIM 
        /// (structural similarity metric, see https://en.wikipedia.org/wiki/SSIM for additional details).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLoggingOptions WithPrintSsim(bool value)
        {
            PrintSsim = value;

            return this;
        }

        /// <summary>
        /// Compute and report local similarity metric 
        /// (sum of lowest error amongst the collocated pixel neighbors).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLoggingOptions WithPrintLsim(bool value)
        {
            PrintLsim = value;

            return this;
        }

        /// <summary>
        /// Report encoding progress in percent.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLoggingOptions WithProgress(bool value)
        {
            Progress = value;

            return this;
        }

        /// <summary>
        /// Do not print anything.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLoggingOptions WithQuiet(bool value)
        {
            Quiet = value;

            return this;
        }

        /// <summary>
        /// Only print brief information (output file size and PSNR) for testing purposes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLoggingOptions WithShort(bool value)
        {
            Short = value;

            return this;
        }

        /// <summary>
        /// Output additional ASCII-map of encoding information. Possible map values range from 1 to 6. 
        /// This is only meant to help debugging.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CWebPLoggingOptions WithMap(int value)
        {
            Map = Clamp(value, 0, 6);

            return this;
        }

        public Dictionary<string, string> GetCommandArguments()
        {
            var args = new Dictionary<string, string>();

            if(Verbose.HasValue && Verbose.Value == true)
                args.Add("-v", null);

            if(PrintPsnr.HasValue && PrintPsnr.Value == true)
                args.Add("-print_psnr", null);

            if(PrintSsim.HasValue && PrintSsim.Value == true)
                args.Add("-print_ssim", null);

            if(PrintLsim.HasValue && PrintLsim.Value == true)
                args.Add("-print_lsim", null);

            if(Progress.HasValue && Progress.Value == true)
                args.Add("-progress", null);

            if(Quiet.HasValue && Quiet.Value == true)
                args.Add("-quiet", null);

            if(Short.HasValue && Short.Value == true)
                args.Add("-short", null);

            if(Map.HasValue)
                args.Add("-map", Map.Value.ToString());

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
