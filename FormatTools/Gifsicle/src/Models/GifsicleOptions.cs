using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.Gifsicle
{
    public class GifsicleOptions
    {
        public string Output { get; private set; }
        public int? Lossyness { get; private set; }
        public int? Optimize { get; private set; }
        public bool? Batch { get; private set; }

        /// <summary>
        /// Send output to file. The special filename ‘-’ means the standard output. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GifsicleOptions WithOutput(string value)
        {
            Output = value;

            return this;
        }

        /// <summary>
        /// Alter image colors to shrink output file size at the cost of artifacts and noise. 
        /// Lossiness determines how many artifacts are allowed; higher values can result in smaller file sizes, 
        /// but cause more artifacts. The default lossiness is 20.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GifsicleOptions WithLossyness(int value)
        {
            Lossyness = Clamp(value, 0, 200);

            return this;
        }

        /// <summary>
        /// Attempt to shrink the file sizes of GIF animations. Level determines how much optimization is done; higher levels take longer, but may have better results. There are currently three levels:
        /// -O1 Store only the changed portion of each image. This is the default.
        /// -O2 Store only the changed portion of each image, and use transparency. 
        /// -O3 Try several optimization methods (usually slower, sometimes better results). 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GifsicleOptions WithOptimize(int value)
        {
            Optimize = Clamp(value, 1, 3);

            return this;
        }

        /// <summary>
        /// Modify each GIF input in place by reading and writing to the same filename. 
        /// (GIFs read from the standard input are written to the standard output.) 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GifsicleOptions WithBatch(bool value)
        {
            Batch = value;

            return this;
        }

        public Dictionary<string, string> GetCommandArguments()
        {
            var args = new Dictionary<string, string>();

            if(!string.IsNullOrEmpty(Output))
                args.Add("--output", Output);

            if(Lossyness.HasValue)
                args.Add($"--lossy={Lossyness.Value}", null);

            if(Optimize.HasValue)
                args.Add($"--optimize={Optimize.Value}", null);

            if(Batch.HasValue && Batch.Value)
                args.Add("--batch", null);

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
