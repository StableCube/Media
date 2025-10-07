using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.ImageMagick.CommandBuilder
{
    /// <summary>
    /// Collection of Image Magick command values
    /// </summary>
    public class CommandOptionSequence
    {
        public SortedDictionary<int, ICommandOption> Options { get; set; } = new SortedDictionary<int, ICommandOption>();

        public int Add(ICommandOption option, int? index = null)
        {
            int idx = 0;
            if(index.HasValue)
                idx = index.Value;

            if(index.HasValue)
            {
                Options.Add(idx, option);
            }
            else
            {
                if(Options.Count > 0)
                    idx = Options.Keys.Last() + 1;

                Options.Add(idx, option);
            }

            return idx;
        }
    }
}