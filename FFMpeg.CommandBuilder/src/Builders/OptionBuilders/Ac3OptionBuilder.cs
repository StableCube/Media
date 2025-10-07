using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.AC3;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class Ac3OptionBuilder : AudioOptionBuilder
    {
        public Ac3OptionBuilder(List<IOption> source) : base(source)
        {
        }

        public Ac3OptionBuilder AddCenterMixLevel(float value, StreamSpecifier stream = null)
        {
            Options.Add(new CenterMixLevelOption(stream)
            {
                Value = value
            });

            return this;
        }

        public Ac3OptionBuilder AddDolbyHeadphoneMode(DolbyHeadphoneMode mode, StreamSpecifier stream = null)
        {
            Options.Add(new DolbyHeadphoneModeOption(stream)
            {
                Mode = mode
            });

            return this;
        }

        public Ac3OptionBuilder AddDolbySurroundEXModemixMode(DolbySurroundEXModemixMode mode, StreamSpecifier stream = null)
        {
            Options.Add(new DolbySurroundEXModemixModeOption(stream)
            {
                Mode = mode
            });

            return this;
        }

        public Ac3OptionBuilder AddLeftRightCenterMixLevel(float level, StreamSpecifier stream = null)
        {
            Options.Add(new LeftRightCenterMixLevelOption(stream)
            {
                Level = level
            });

            return this;
        }

        public Ac3OptionBuilder AddLeftRightSurroundMixLevel(float level, StreamSpecifier stream = null)
        {
            Options.Add(new LeftRightSurroundMixLevelOption(stream)
            {
                Level = level
            });

            return this;
        }

        public Ac3OptionBuilder AddStereoDownmixMode(StereoDownmixMode mode, StreamSpecifier stream = null)
        {
            Options.Add(new StereoDownmixModeOption(stream)
            {
                Mode = mode
            });

            return this;
        }

        public Ac3OptionBuilder AddStereoRematrixing(bool enabled, StreamSpecifier stream = null)
        {
            Options.Add(new StereoRematrixingOption(stream)
            {
                Enabled = enabled
            });

            return this;
        }

        public Ac3OptionBuilder AddSurroundMixLevel(float level, StreamSpecifier stream = null)
        {
            Options.Add(new SurroundMixLevelOption(stream)
            {
                Level = level
            });

            return this;
        }
    }
}