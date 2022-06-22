using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class Libx264OptionBuilder : VideoOptionBuilder
    {
        public Libx264OptionBuilder(List<IOption> source) : base(source)
        {
        }

        public Libx264OptionBuilder AddA53ClosedCaptions(bool enabled, StreamSpecifier stream = null)
        {
            Options.Add(new A53ClosedCaptionsOption(stream)
            {
                Enabled = enabled
            });

            return this;
        }

        public Libx264OptionBuilder AddConstantRateFactor(int rate, StreamSpecifier stream = null)
        {
            Options.Add(new ConstantRateFactorOption(stream)
            {
                Value = rate
            });

            return this;
        }

        public Libx264OptionBuilder AddConstantRateFactorMax(int maxRate, StreamSpecifier stream = null)
        {
            Options.Add(new ConstantRateFactorMaxOption(stream)
            {
                Value = maxRate
            });

            return this;
        }

        public Libx264OptionBuilder AddLevel(Level level, StreamSpecifier stream = null)
        {
            Options.Add(new LevelOption(stream)
            {
                LevelValue = level
            });

            return this;
        }

        public Libx264OptionBuilder AddParameters(List<SerializableParameter> parameters, StreamSpecifier stream = null)
        {
            Options.Add(new ParametersOption(stream)
            {
                Parameters = parameters
            });

            return this;
        }

        public Libx264OptionBuilder AddPFrameWeightedPrediction(int value, StreamSpecifier stream = null)
        {
            Options.Add(new PFrameWeightedPredictionOption(stream)
            {
                Value = value
            });

            return this;
        }

        public Libx264OptionBuilder AddPreset(Preset preset, StreamSpecifier stream = null)
        {
            Options.Add(new PresetOption(stream)
            {
                PresetValue = preset
            });

            return this;
        }

        public Libx264OptionBuilder AddPreset(string preset, StreamSpecifier stream = null)
        {
            Options.Add(new PresetOption(stream)
            {
                PresetValue = PresetOption.PresetFromString(preset)
            });

            return this;
        }

        public Libx264OptionBuilder AddProfile(Profile profile, StreamSpecifier stream = null)
        {
            Options.Add(new ProfileOption(stream)
            {
                ProfileValue = profile
            });

            return this;
        }

        public Libx264OptionBuilder AddProfile(string profile, StreamSpecifier stream = null)
        {
            Options.Add(new ProfileOption(stream)
            {
                ProfileValue = ProfileOption.ProfileFromString(profile)
            });

            return this;
        }

        public Libx264OptionBuilder AddQuantizationParameter(int value, StreamSpecifier stream = null)
        {
            Options.Add(new QuantizationParameterOption(stream)
            {
                Value = value
            });

            return this;
        }

        public Libx264OptionBuilder AddTune(Tune tune, StreamSpecifier stream = null)
        {
            Options.Add(new TuneOption(stream)
            {
                TuneValue = tune
            });

            return this;
        }

        public Libx264OptionBuilder AddTune(string tune, StreamSpecifier stream = null)
        {
            Options.Add(new TuneOption(stream)
            {
                TuneValue = TuneOption.TuneFromString(tune)
            });

            return this;
        }
    }
}