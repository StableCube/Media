using System;
using System.IO;
using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Main;

namespace StableCube.Media.FFMpeg.CommandFactory
{
    public static class FrameExtractionTaskFactory
    {
        /// <summary>
        /// Extracts frames at intervals in seconds.
        /// </summary>
        public static FFMpegCommandTask CreateExtractFrameAtIntervalTask(
            string sourceFilePath,
            string outDir,
            string filenamePrefix,
            ImageFormatType outFormat,
            TimeSpan interval,
            ScaleParameters scaleOutputParams = null
        )
        {
            string ext = VideoMiscMapper.ImageTypeToExtension(outFormat);
            var outFile = Path.Combine(outDir, $"{filenamePrefix}%04d.{ext}");

            CommandTaskBuilder taskBuilder = new CommandTaskBuilder();
            var optGroupBuilder = taskBuilder.BuildOptionGroup(0)
                .AddInput(sourceFilePath)
                .AddOutput(outFile);

            optGroupBuilder.OutputScope.MainOptions
                .AddVideoSync(VideoSyncMethod.Passthrough, StreamSpecifier.Video)
                .AudioOptions.AddDisableAudio();

            var chain = optGroupBuilder.FilterGraphScope
                .BuildFilterChain()
                .AddInputLink(new StreamSpecifier('v', 0))
                .AddFPS(1)
                .AddSelect($"expr='not(mod(t,{interval.TotalSeconds}))'");

            if(scaleOutputParams != null)
            {
                chain.AddOutputLink("extract");

                optGroupBuilder.FilterGraphScope
                    .BuildFilterChain()
                    .AddDynamicScale(scaleOutputParams)
                    .AddInputLink("extract");
            }

            return taskBuilder.Task;
        }

        /// <summary>
        /// Extract number of frames evenly spaced throughout video
        /// </summary>
        public static FFMpegCommandTask CreateExtractFramesRangeTask(
            string sourceFilePath,
            TimeSpan sourceDuration,
            string outDir,
            ImageFormatType outFormat,
            string filenamePrefix,
            int extractFrameCount,
            ScaleParameters scaleOutputParams = null
        )
        {
            int timeCount = extractFrameCount + 2;
            double frameInterval = sourceDuration.TotalMilliseconds / ((double)timeCount);
            List<TimeSpan> extractTimes = new List<TimeSpan>();

            for (int i = 0; i < timeCount; i++)
            {
                extractTimes.Add(TimeSpan.FromMilliseconds(frameInterval * (double)i));
            }

            // Throw away the first and last frame to avoid useless frames
            if(extractTimes.Count > 2)
            {
                extractTimes.RemoveAt(timeCount - 1);
                extractTimes.RemoveAt(0);
            }

            return CreateExtractFramesTask(
                sourceFilePath: sourceFilePath,
                outDir: outDir,
                outFormat: outFormat,
                filenamePrefix: filenamePrefix,
                extractTimes: extractTimes,
                scaleOutputParams: scaleOutputParams
            );
        }

        /// <summary>
        /// Efficiently extracts many frames at given time points
        /// </summary>
        public static FFMpegCommandTask CreateExtractFramesTask(
            string sourceFilePath,
            string outDir,
            ImageFormatType outFormat,
            string filenamePrefix,
            IEnumerable<TimeSpan> extractTimes,
            ScaleParameters scaleOutputParams = null
        )
        {
            CommandTaskBuilder taskBuilder = new CommandTaskBuilder();

            string ext = VideoMiscMapper.ImageTypeToExtension(outFormat);

            int i = 0;
            foreach (var extractTime in extractTimes)
            {
                long milliseconds = Convert.ToInt64(extractTime.TotalMilliseconds);
                string outFile = Path.Combine(outDir, $"{filenamePrefix}{milliseconds.ToString("D19")}.{ext}");

                var optGroupBuilder = taskBuilder.BuildOptionGroup(i)
                    .AddInput(sourceFilePath)
                    .AddOutput(outFile);

                optGroupBuilder.InputScope(0).MainOptions
                    .AddSeek(extractTime);

                optGroupBuilder.OutputScope.MainOptions
                    .AddFrames(1, StreamSpecifier.Video);

                if(scaleOutputParams != null)
                {
                    optGroupBuilder.FilterGraphScope
                        .BuildFilterChain()
                        .AddDynamicScale(scaleOutputParams)
                        .AddInputLink($"{i}:v")
                        .AddOutputLink($"scaled{i}")
                        ;

                    optGroupBuilder.OutputScope.MainOptions
                        .AddMap($"[scaled{i}]");
                }
                else
                {
                    optGroupBuilder.OutputScope.MainOptions
                        .AddMap($"{i}:v");
                }

                i++;
            }

            return taskBuilder.Task;
        }

        /// <summary>
        /// Extracts a frame at a point in time
        /// </summary>
        public static FFMpegCommandTask CreateExtractFrameAtTimeTask(
            string sourceFilePath,
            string outFile,
            TimeSpan time,
            ScaleParameters scaleOutputParams = null
        )
        {
            CommandTaskBuilder taskBuilder = new CommandTaskBuilder();
            var optGroupBuilder = taskBuilder.BuildOptionGroup(0);
            
            optGroupBuilder
                .AddInput(sourceFilePath)
                .AddOutput(outFile)
                .GlobalScope.MainOptions
                // Seek to point in source. This is fastest if before input
                .AddSeek(time)
                .AddVideoSync(VideoSyncMethod.Passthrough, StreamSpecifier.Video) 
                .AudioOptions
                // No need for audio in img output
                .AddDisableAudio();

            optGroupBuilder.OutputScope.MainOptions
                // Only output single frame
                .AddFrames(1, StreamSpecifier.Video);

            if(scaleOutputParams != null && scaleOutputParams.ScalingMode != FFMpegScalingMode.Source)
            {
                optGroupBuilder.FilterGraphScope
                    .BuildFilterChain()
                    .AddInputLink(new StreamSpecifier('v', 0))
                    .AddDynamicScale(scaleOutputParams);
            }

            return taskBuilder.Task;
        }
    }
}