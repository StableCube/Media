using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class OptionBuilder : ParameterProvider
    {
        public List<IOption> Options { get; private set; } = new List<IOption>();

        /// <summary>
        /// Modify generic option set
        /// </summary>
        /// <link>https://ffmpeg.org/ffmpeg.html#Generic-options</link>
        public GenericOptionBuilder GenericOptions 
        { 
            get 
            { 
                return new GenericOptionBuilder(Options); 
            }
        }

        /// <summary>
        /// Modify main option set
        /// </summary>
        /// <link>https://ffmpeg.org/ffmpeg.html#Main-options</link>
        public MainOptionBuilder MainOptions 
        { 
            get 
            { 
                return new MainOptionBuilder(Options); 
            }
        }

        /// <summary>
        /// Modify FFProbe specific option set
        /// </summary>
        /// <link>https://ffmpeg.org/ffprobe.html#Options</link>
        public ProbeOptionBuilder ProbeOptions 
        { 
            get 
            { 
                return new ProbeOptionBuilder(Options); 
            }
        }

        public VideoOptionBuilder VideoOptions
        { 
            get 
            { 
                return new VideoOptionBuilder(Options); 
            }
        }

        public AudioOptionBuilder AudioOptions
        { 
            get 
            { 
                return new AudioOptionBuilder(Options); 
            }
        }

        public MuxerOptionBuilder MuxerOptions
        { 
            get 
            { 
                return new MuxerOptionBuilder(Options); 
            }
        }

        public Ac3OptionBuilder Ac3Options
        { 
            get 
            { 
                return new Ac3OptionBuilder(Options); 
            }
        }

        public Libx264OptionBuilder Libx264Options
        { 
            get 
            { 
                return new Libx264OptionBuilder(Options); 
            }
        }

        public Libx265OptionBuilder Libx265Options
        { 
            get 
            { 
                return new Libx265OptionBuilder(Options); 
            }
        }

        public OptionBuilder()
        {
        }

        public OptionBuilder(List<IOption> source)
        {
            Options = source;
        }

        public override List<CommandParam> GetParameters()
        {
            List<CommandParam> optionParams = new List<CommandParam>();
            foreach (var option in Options)
            {
                optionParams.Add(option.GetCommandParameter());
            }

            return optionParams;
        }
    }
}