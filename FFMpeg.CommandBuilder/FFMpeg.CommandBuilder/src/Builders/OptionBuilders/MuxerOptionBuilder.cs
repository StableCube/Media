using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Muxer;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class MuxerOptionBuilder : OptionBuilder
    {
        public MuxerOptionBuilder(List<IOption> source) : base(source)
        {
        }

        public MuxerOptionBuilder AddFramePTS(bool enable)
        {
            Options.Add(new FramePTSOption()
            {
                FramePts = enable
            });

            return this;
        }

        public MuxerOptionBuilder AddMoveFlags(FlagType flag)
        {
            Options.Add(new MoveFlagsOption()
            {
                Flag = flag
            });

            return this;
        }

        public MuxerOptionBuilder AddStartNumber(int number)
        {
            Options.Add(new StartNumberOption()
            {
                Number = number
            });

            return this;
        }

        public MuxerOptionBuilder AddStringFormatTime(bool enable = true)
        {
            Options.Add(new StringFormatTimeOption()
            {
                Enabled = enable
            });

            return this;
        }
    }
}