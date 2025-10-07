using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Audio;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class AudioOptionBuilder : OptionBuilder
    {
        public AudioOptionBuilder()
        {
        }

        public AudioOptionBuilder(List<IOption> source) : base(source)
        {
        }

        public AudioOptionBuilder AddAudioChannels(int channelCount, StreamSpecifier stream = null)
        {
            Options.Add(new AudioChannelsOption(stream)
            {
                ChannelCount = channelCount
            });

            return this;
        }

        public AudioOptionBuilder AddAudioSamplingFrequency(string frequency, StreamSpecifier stream = null)
        {
            Options.Add(new AudioSamplingFrequencyOption(stream)
            {
                Frequency = frequency
            });

            return this;
        }

        public AudioOptionBuilder AddDisableAudio()
        {
            Options.Add(new DisableAudioOption());

            return this;
        }

        public AudioOptionBuilder AddSampleFormat(SampleFormat format, StreamSpecifier stream = null)
        {
            Options.Add(new SampleFormatOption(stream)
            {
                Format = format
            });

            return this;
        }

        public AudioOptionBuilder AddAudioCodec(string codec, StreamSpecifier stream = null)
        {
            Options.Add(new ACodecOption(stream)
            {
                Codec = codec
            });

            return this;
        }
    }
}