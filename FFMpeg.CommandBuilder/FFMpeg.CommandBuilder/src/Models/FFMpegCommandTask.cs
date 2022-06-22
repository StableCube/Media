using System;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Command tasks are individual FFMpeg commands
    /// </summary>
    public class FFMpegCommandTask
    {
        public SortedDictionary<int, FFMpegCommandOptionGroup> CommandOptionGroups { get; set; }
            = new SortedDictionary<int, FFMpegCommandOptionGroup>();

        /// <summary>
        /// Terminal command string
        /// </summary>
        public override string ToString()
        {
            List<string> values = new List<string>();
            foreach (var pair in CommandOptionGroups)
            {
                values.Add(pair.Value.ToString());
            }

            return String.Join(" ", values);
        }
    }
}