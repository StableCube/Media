using System;
using StableCube.Media.FFMpeg.CommandBuilder;

namespace StableCube.Media.FFMpeg.CommandFactory
{
    public static class VideoPreviewTaskFactory
    {
        /// <summary>
        /// Builds an FFMpeg command to make a preview video by stitching together short
        /// segments from throughout the video
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputFile"></param>
        /// <param name="sourceDuration">Playtime of source video</param>
        /// <param name="videoStream">Video stream to sample</param>
        /// <param name="outputDimensions">Dimensions for the output file</param>
        /// <param name="chunkCount">Number of segments of the video to sample</param>
        /// <param name="chunkDuration">Length of each segment chunk</param>
        /// <returns>FFMpegCommandTask</returns>
        public static FFMpegCommandTask CreateSegmentedVideoPreviewTask(
            string inputFile, 
            string outputFile,
            TimeSpan sourceDuration,
            StreamSpecifier videoStream,
            int chunkCount,
            TimeSpan chunkDuration,
            ScaleParameters scaleOutputParams = null
        )
        {
            CommandTaskBuilder cmdBuilder = new CommandTaskBuilder();
            CommandOptionGroupBuilder optGroupBuilder = cmdBuilder
                .BuildOptionGroup(0)
                .AddInput(0, inputFile)
                .AddOutput(0, outputFile);

            double chunkOffset = Math.Floor(sourceDuration.TotalSeconds / (double)chunkCount);

            var chain = optGroupBuilder.FilterGraphScope.BuildFilterChain()
                .AddInputLink(videoStream)
                .AddOutputLink("outv");

            optGroupBuilder.OutputScope
                .MainOptions.AddMap("[outv]");

            if(Math.Floor(sourceDuration.TotalSeconds) > chunkDuration.TotalSeconds * chunkCount)
            {
                string[] betweenParts = new string[chunkCount];
                for (int i = 0; i < chunkCount; i++)
                    betweenParts[i] = $"between(t,{chunkOffset * i},{chunkOffset * i + chunkDuration.TotalSeconds})";

                chain.AddSelect($"'{string.Join("+", betweenParts)}'")
                    .AddSetPTS("N/FRAME_RATE/TB");
            }

            if(scaleOutputParams != null)
            {
                chain.AddDynamicScale(scaleOutputParams);
                //Make sure output width/height is divisible by 2 to prevent problems
                chain.AddPad(@"iw+mod(iw\,2)", @"ih+mod(ih\,2)", null, null, null);
            }

            return cmdBuilder.Task;
        }
    }
}