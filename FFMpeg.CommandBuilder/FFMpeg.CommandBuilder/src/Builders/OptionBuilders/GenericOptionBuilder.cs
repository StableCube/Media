using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class GenericOptionBuilder : OptionBuilder
    {
        public GenericOptionBuilder()
        {
        }

        public GenericOptionBuilder(List<IOption> source) : base(source)
        {
        }

        public GenericOptionBuilder AddHideBanner()
        {
            Options.Add(new HideBannerOption());

            return this;
        }

        public GenericOptionBuilder AddLogLevel(FFMpegLogLevel level, bool repeat = false, bool levelPrefix = false)
        {
            Options.Add(new LogLevelOption()
            {
                LogingLevel = level,
                Repeat = repeat,
                LevelPrefix = levelPrefix
            });

            return this;
        }

        public GenericOptionBuilder AddOverwriteOutputYes()
        {
            Options.Add(new OverwriteOutputYesOption());

            return this;
        }

        public GenericOptionBuilder AddOverwriteOutputNo()
        {
            Options.Add(new OverwriteOutputNoOption());

            return this;
        }
    }
}