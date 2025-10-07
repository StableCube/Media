using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.OptiPng
{
    public class OptiPngOptions
    {
        public string OutputFile { get; private set; }
        public string OutputDir { get; private set; }
        public bool? Clobber { get; private set; }
        public bool? Backup { get; private set; }
        public bool? Fix { get; private set; }
        public bool? Force { get; private set; }
        public bool? Preserve { get; private set; }
        public bool? Quiet { get; private set; }
        public bool? Simulate { get; private set; }
        public bool? Verbose { get; private set; }
        public int? OptimizationLevel { get; private set; }

        /// <summary>
        /// write output file to <file>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithOutputFile(string value)
        {
            OutputFile = value;

            return this;
        }

        /// <summary>
        /// write output file(s) to <directory>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithOutputDir(string value)
        {
            OutputDir = value;

            return this;
        }

        /// <summary>
        /// overwrite existing files
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithClobber(bool value)
        {
            Clobber = value;

            return this;
        }

        /// <summary>
        /// keep a backup of the modified files
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithBackup(bool value)
        {
            Backup = value;

            return this;
        }

        /// <summary>
        /// enable error recovery
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithFix(bool value)
        {
            Fix = value;

            return this;
        }

        /// <summary>
        /// enforce writing of a new output file
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithForce(bool value)
        {
            Force = value;

            return this;
        }

        /// <summary>
        /// preserve file attributes if possible
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithPreserve(bool value)
        {
            Preserve = value;

            return this;
        }

        /// <summary>
        /// run in quiet mode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithQuiet(bool value)
        {
            Quiet = value;

            return this;
        }

        /// <summary>
        /// run in simulation mode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithSimulate(bool value)
        {
            Simulate = value;

            return this;
        }

        /// <summary>
        /// optimization level (0-7)		[default: 2]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>/
        public OptiPngOptions WithOptimizationLevel(int value)
        {
            OptimizationLevel = Clamp(value, 0, 7);

            return this;
        }

        /// <summary>
        /// run in verbose mode / show copyright and version info
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public OptiPngOptions WithVerbose(bool value)
        {
            Verbose = value;

            return this;
        }

        public Dictionary<string, string> GetCommandArguments()
        {
            var args = new Dictionary<string, string>();

            if(OutputFile != null)
                args.Add("-out", OutputFile);

            if(OutputDir != null)
                args.Add("-dir", OutputDir);

            if(Clobber.HasValue && Clobber.Value == true)
                args.Add("-clobber", null);

            if(Backup.HasValue && Backup.Value == true)
                args.Add("-backup", null);

            if(Fix.HasValue && Fix.Value == true)
                args.Add("-fix", null);

            if(Force.HasValue && Force.Value == true)
                args.Add("-force", null);

            if(Preserve.HasValue && Preserve.Value == true)
                args.Add("-preserve", null);

            if(Quiet.HasValue && Quiet.Value == true)
                args.Add("-quiet", null);

            if(Simulate.HasValue && Simulate.Value == true)
                args.Add("-simulate", null);

            if(OptimizationLevel.HasValue)
                args.Add($"-o{OptimizationLevel.Value}", null);

            if(Verbose.HasValue && Verbose.Value == true)
                args.Add("-v", null);

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
