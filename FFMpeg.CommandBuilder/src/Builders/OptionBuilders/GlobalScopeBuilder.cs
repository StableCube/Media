using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class GlobalScopeBuilder : ParameterProvider
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

        public GlobalScopeBuilder()
        {
        }

        public GlobalScopeBuilder(List<IOption> source)
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