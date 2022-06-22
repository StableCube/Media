using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using StableCube.Media.FFMpeg.CommandBuilder;
using StableCube.Media.FFMpeg.DataModel;

namespace StableCube.Media.FFMpeg.Tests
{
    public class FFMpegServiceTests : IDisposable
    {
        private static string _buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string _projectRoot = Path.Combine(_buildDir, "..", "..", "..");
        private static string _testDataRoot = Path.Combine(_projectRoot, "data");
        private static string _outPathDir = Path.Combine(_projectRoot, "data", "test_out_DELETE_ME");

        public FFMpegServiceTests()
        {
            if(!Directory.Exists(_outPathDir))
                Directory.CreateDirectory(_outPathDir);
        }

        public void Dispose()
        {
            if(Directory.Exists(_outPathDir))
                Directory.Delete(_outPathDir, true);
        }

        [Fact]
        public async Task Should_Transcode_To_Output_File()
        {
            string filePath = Path.Combine(_testDataRoot, "nature-360p.mp4");
            string outPath = Path.Combine(_outPathDir, "nature-360p.mp4");
            var ffmpeg = new FFMpegService();

            var cmd = new FFMpegCommandTask();
            var optionGroup = new FFMpegCommandOptionGroup(0);
            optionGroup.AddInputFile(filePath);
            optionGroup.AddOutputFile(outPath);

            cmd.CommandOptionGroups = new SortedDictionary<int, FFMpegCommandOptionGroup>()
            {
                { 0, optionGroup },
            };

            var cmdResult = await ffmpeg.RunCommandTaskAsync(cmd);

            Assert.Equal(0, cmdResult.ExitCode);
            Assert.True(File.Exists(outPath));
        }

        [Fact]
        public async Task Should_Transcode_With_Progress_Logs()
        {
            string filePath = Path.Combine(_testDataRoot, "nature-360p.mp4");
            string outPath = Path.Combine(_outPathDir, "nature-360p.mp4");
            string logPath = Path.Combine(_outPathDir, "test.log");
            var ffmpeg = new FFMpegService();

            var cmd = new FFMpegCommandTask();
            var optionGroup = new FFMpegCommandOptionGroup(0);
            optionGroup.AddInputFile(filePath);
            optionGroup.AddOutputFile(outPath);

            cmd.CommandOptionGroups = new SortedDictionary<int, FFMpegCommandOptionGroup>()
            {
                { 0, optionGroup },
            };

            var progressItems = new List<int>();

            var progress = new Progress<ProgressResult>((progressResult) => {
                progressItems.Add(progressResult.Progress);
            });

            FFMpegLogOptions logOpts = new FFMpegLogOptions()
            {
                LogFilePath = logPath,
                SourceFilePath = filePath,
                ProgressData = progress
            };
            
            var cmdResult = await ffmpeg.RunCommandTaskAsync(cmd, logOpts);

            Assert.Equal(0, cmdResult.ExitCode);
            Assert.True(progressItems.Count > 1);
            Assert.True(progressItems[1] > 1);
        }
    }
}