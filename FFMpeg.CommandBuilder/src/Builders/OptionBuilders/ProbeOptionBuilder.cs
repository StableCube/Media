using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Probe;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class ProbeOptionBuilder : OptionBuilder
    {
        public ProbeOptionBuilder()
        {
        }

        public ProbeOptionBuilder(List<IOption> source) : base(source)
        {
        }

        public ProbeOptionBuilder AddCountFrames()
        {
            Options.Add(new CountFramesOption());

            return this;
        }

        public ProbeOptionBuilder AddOutputFormat(string format)
        {
            Options.Add(new OutputFormatOption()
            {
                Format = format
            });

            return this;
        }

        public ProbeOptionBuilder AddSelectStreams(StreamSpecifier stream)
        {
            Options.Add(new SelectStreamsOption()
            {
                Stream = stream
            });

            return this;
        }

        public ProbeOptionBuilder AddShowEntries(string entries)
        {
            Options.Add(new ShowEntriesOption()
            {
                Entries = entries
            });

            return this;
        }

        public ProbeOptionBuilder AddShowFormat()
        {
            Options.Add(new ShowFormatOption());

            return this;
        }

        public ProbeOptionBuilder AddShowFrames()
        {
            Options.Add(new ShowFramesOption());

            return this;
        }

        public ProbeOptionBuilder AddShowStreams()
        {
            Options.Add(new ShowStreamsOption());

            return this;
        }
    }
}