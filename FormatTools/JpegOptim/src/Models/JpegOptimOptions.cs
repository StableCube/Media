using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.JpegOptim
{
    public class JpegOptimOptions
    {
        public string Destination { get; private set; }
        public bool? Force { get; private set; }
        public int? MaxQualityFactor { get; private set; }
        public int? Threshold { get; private set; }
        public bool? Csv { get; private set; }
        public bool? Overwrite { get; private set; }
        public bool? Quiet { get; private set; }
        public bool? Totals { get; private set; }
        public bool? Verbose { get; private set; }
        public bool? AllNormal { get; private set; }
        public bool? AllProgressive { get; private set; }
        public bool? StripAll { get; private set; }
        public bool? StripNone { get; private set; }

        /// <summary>
        /// Sets alternative destination directory where to save optimized files (default is to overwrite the originals). 
        /// Please note that unchanged files won't be added to the destination directory. This means if the 
        /// source file can't be compressed, no file will be created in the destination path.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>/
        public JpegOptimOptions WithDestination(string value)
        {
            Destination = value;

            return this;
        }

        /// <summary>
        /// Force optimization, even if the result would be larger than the original file.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithForce(bool value)
        {
            Force = value;

            return this;
        }

        /// <summary>
        /// Sets the maximum image quality factor (disables lossless optimization mode, which is by 
        /// default enabled). This option will reduce quality of those source files that were saved 
        /// using higher quality setting.  While files that already have lower quality setting 
        /// will be compressed using the lossless optimization method.
        ///
        /// Valid values for quality parameter are: 0 - 100
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithMaxQualityFactor(int value)
        {
            MaxQualityFactor = Clamp(value, 0, 100);

            return this;
        }

        /// <summary>
        /// Keep the file unchanged if the compression gain is lower than the threshold (%).
        /// 
        /// Valid values for threshold are: 0 - 100
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithThreshold(int value)
        {
            Threshold = Clamp(value, 0, 100);

            return this;
        }

        /// <summary>
        /// Print progress info in CSV format.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithCsv(bool value)
        {
            Csv = value;

            return this;
        }

        /// <summary>
        /// Overwrite target file even if it exists (when using -d option).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithOverwrite(bool value)
        {
            Overwrite = value;

            return this;
        }

        /// <summary>
        /// Quiet mode.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithQuiet(bool value)
        {
            Quiet = value;

            return this;
        }

        /// <summary>
        /// Print totals after processing all files.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithTotals(bool value)
        {
            Totals = value;

            return this;
        }

        /// <summary>
        /// Enables verbose mode (positively chatty).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithVerbose(bool value)
        {
            Verbose = value;

            return this;
        }

        /// <summary>
        /// Force all output files to be non-progressive. Can be used to convert all input files to 
        /// progressive JPEGs when used with --force option.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithAllNormal(bool value)
        {
            AllNormal = value;

            return this;
        }

        /// <summary>
        /// Force all output files to be progressive. Can be used to convert  all normal (non-progressive) 
        /// JPEGs input files to progressive when used with --force option.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithAllProgressive(bool value)
        {
            AllProgressive = value;

            return this;
        }

        /// <summary>
        /// Strip all markers from output file. 
        /// (NOTE! by default only Comment & Exif/IPTC/PhotoShop/ICC/XMP markers are kept, everything else is discarded). 
        /// Output JPEG still likely will contains one or two markers (JFIF and Adobe APP14) depending on colorspace used in the image, as these markers 
        /// are generated by the libjpeg encoder  automatically.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithStripAll(bool value)
        {
            StripAll = value;

            return this;
        }

        /// <summary>
        /// Preserve "all" markers in the image. This will leave all markers untouched in the image, except JFIF (APP0) and Adobe (APP14) markers as those get regenerated by the libjpeg library.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public JpegOptimOptions WithStripNone(bool value)
        {
            StripNone = value;

            return this;
        }

        public Dictionary<string, string> GetCommandArguments()
        {
            var args = new Dictionary<string, string>();

            if(!string.IsNullOrEmpty(Destination))
                args.Add($"--dest={Destination}", null);

            if(Force.HasValue && Force.Value)
                args.Add("-f", null);

            if(MaxQualityFactor.HasValue)
                args.Add($"--size={MaxQualityFactor.Value.ToString()}", null);

            if(Threshold.HasValue)
                args.Add($"--threshold={Threshold.Value.ToString()}", null);

            if(Csv.HasValue && Csv.Value)
                args.Add("--csv", null);

            if(Overwrite.HasValue && Overwrite.Value)
                args.Add("--overwrite", null);

            if(Quiet.HasValue && Quiet.Value)
                args.Add("--quiet", null);

            if(Totals.HasValue && Totals.Value)
                args.Add("--totals", null);

            if(Verbose.HasValue && Verbose.Value)
                args.Add("--verbose", null);

            if(AllNormal.HasValue && AllNormal.Value)
                args.Add("--all-normal", null);

            if(AllProgressive.HasValue && AllProgressive.Value)
                args.Add("--all-progressive", null);

            if(StripAll.HasValue && StripAll.Value)
                args.Add("--strip-all", null);

            if(StripNone.HasValue && StripNone.Value)
                args.Add("--strip-none", null);

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
