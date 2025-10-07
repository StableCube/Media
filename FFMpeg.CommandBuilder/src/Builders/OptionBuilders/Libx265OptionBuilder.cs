using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Libx265;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class Libx265OptionBuilder : VideoOptionBuilder
    {
        public Libx265OptionBuilder(List<IOption> source) : base(source)
        {
        }

        public Libx265OptionBuilder AddConstantRateFactor(int rate, StreamSpecifier stream = null)
        {
            Options.Add(new ConstantRateFactorOption(stream)
            {
                Value = rate
            });

            return this;
        }

        public Libx265OptionBuilder AddConstantRateFactor(List<SerializableParameter> parameters, StreamSpecifier stream = null)
        {
            Options.Add(new ParametersOption(stream)
            {
                Parameters = parameters
            });

            return this;
        }

        public Libx265OptionBuilder AddPreset(Preset preset, StreamSpecifier stream = null)
        {
            Options.Add(new PresetOption(stream)
            {
                Value = preset
            });

            return this;
        }

        public Libx265OptionBuilder AddTune(Tune tune, StreamSpecifier stream = null)
        {
            Options.Add(new TuneOption(stream)
            {
                Value = tune
            });

            return this;
        }
    }
}