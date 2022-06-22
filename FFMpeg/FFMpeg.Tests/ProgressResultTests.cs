using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using StableCube.Media.FFMpeg.DataModel;

namespace StableCube.Media.FFMpeg.Tests
{
    public class ProgressResultTests
    {
        private static string _buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string _testDataRoot = Path.Combine(_buildDir, "../../..", "data");

        public ProgressResultTests()
        {
        }

        [Fact]
        public async Task Should_Calculate_Progress_Percentage_As_55()
        {
            string filePath = Path.Combine(_testDataRoot, "fifty_percent_progress.txt");
            List<FFMpegProgressLogEntry> entries = await FFMpegProgressLog.ParseLogFileAsync(filePath);
            FFMpegProgressLogEntry log = entries[entries.Count - 1];

            ProgressResult result = new ProgressResult(
                TimeSpan.FromMilliseconds(42041), 
                log.OutTime, 
                log.Speed
            );

            Assert.Equal(55, result.Progress);
        }
    }
}