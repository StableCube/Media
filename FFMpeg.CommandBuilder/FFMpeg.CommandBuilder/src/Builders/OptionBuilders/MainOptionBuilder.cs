using System;
using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Main;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class MainOptionBuilder : OptionBuilder
    {
        public MainOptionBuilder()
        {
        }

        public MainOptionBuilder(List<IOption> source) : base(source)
        {
        }

        public MainOptionBuilder AddAccurateSeek(bool accurateSeek)
        {
            if(accurateSeek)
            {
                Options.Add(new AccurateSeekYesOption());
            }
            else
            {
                Options.Add(new AccurateSeekNoOption());
            }

            return this;
        }

        public MainOptionBuilder AddBitrate(int bitrate, StreamSpecifier stream = null)
        {
            Options.Add(new BitrateOption(stream)
            {
                Bitrate = bitrate
            });

            return this;
        }

        public MainOptionBuilder AddCodec(string codecName, StreamSpecifier stream = null)
        {
            Options.Add(new CodecOption(stream)
            {
                CodecName = codecName
            });

            return this;
        }

        public MainOptionBuilder AddCopyTimestamp()
        {
            Options.Add(new CopyTimeStampOption());

            return this;
        }

        public MainOptionBuilder AddFilter(FilterGraph filterGraph, StreamSpecifier stream = null)
        {
            Options.Add(new FilterOption(stream)
            {
                FilterGraph = filterGraph
            });

            return this;
        }

        public MainOptionBuilder AddFrames(int frameCount, StreamSpecifier stream = null)
        {
            Options.Add(new FramesOption(stream)
            {
                FrameCount = frameCount
            });

            return this;
        }

        public MainOptionBuilder AddInput(string filePath)
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            Options.Add(new InputOption()
            {
                Path = filePath
            });

            return this;
        }

        public MainOptionBuilder AddMap(string map)
        {
            Options.Add(new MapOption()
            {
                Map = map
            });

            return this;
        }

        public MainOptionBuilder AddProgress(string filePath)
        {
            Options.Add(new ProgressOption()
            {
                FilePath = filePath
            });

            return this;
        }

        public MainOptionBuilder AddSeek(TimeSpan position)
        {
            string posStr = position.ToString();

            Options.Add(new SeekOption()
            {
                Position = posStr
            });

            return this;
        }

        public MainOptionBuilder AddShortest()
        {
            Options.Add(new ShortestOption());

            return this;
        }

        public MainOptionBuilder AddVideoSync(VideoSyncMethod method, StreamSpecifier stream = null)
        {
            Options.Add(new VideoSyncOption(stream)
            {
                Method = method
            });

            return this;
        }
    }
}