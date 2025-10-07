using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.WebP
{
    public class WebPMuxOptions
    {
        private string OutputFile { get; set; }
        private string GetOptions { get; set; }
        private string SetOptions { get; set; }
        private string Strip { get; set; }
        private string Duration { get; set; }
        private SortedDictionary<int, string> Frames { get; set; }

        /// <summary>
        /// Output file in WebP format.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebPMuxOptions WithOutput(string value)
        {
            OutputFile = value;

            return this;
        }

        /// <summary>
        /// GET_OPTIONS (-get)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebPMuxOptions WithGet(string value)
        {
            GetOptions = value;

            return this;
        }

        /// <summary>
        /// SET_OPTIONS (-set)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebPMuxOptions WithSet(string value)
        {
            SetOptions = value;

            return this;
        }

        /// <summary>
        /// STRIP_OPTIONS (-strip)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebPMuxOptions WithStrip(string value)
        {
            Strip = value;

            return this;
        }

        /// <summary>
        /// DURATION_OPTIONS (-duration)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public WebPMuxOptions WithDuration(string value)
        {
            Duration = value;

            return this;
        }

        /// <summary>
        /// FRAME_OPTIONS (-frame)
        /// Create an animated WebP file from multiple (non-animated) WebP images.
        /// </summary>
        /// <param name="value"><index, frame value></param>
        /// <returns></returns>
        public WebPMuxOptions WithFrames(SortedDictionary<int, string> value)
        {
            Frames = value;

            return this;
        }

        public Dictionary<string, string> GetCommandArguments()
        {
            var args = new Dictionary<string, string>();

            if(!string.IsNullOrEmpty(OutputFile))
                args.Add("-o", OutputFile);

            if(!string.IsNullOrEmpty(GetOptions))
                args.Add("-get", GetOptions);

            if(!string.IsNullOrEmpty(SetOptions))
                args.Add("-set", SetOptions);

            if(!string.IsNullOrEmpty(Strip))
                args.Add("-strip", Strip);

            if(!string.IsNullOrEmpty(Duration))
                args.Add("-duration", Duration);

            if(Frames != null)
            {
                var frameStrBuilder = new StringBuilder();
                foreach (var item in Frames)
                    frameStrBuilder.Append($" -frame {item.Value}");
                
                args.Add(frameStrBuilder.ToString(), null);
            }

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
