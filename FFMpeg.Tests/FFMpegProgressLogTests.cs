using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using StableCube.Media.FFMpeg.DataModel;

namespace StableCube.Media.FFMpeg.Tests
{
    public class FFMpegProgressLogTests
    {
        private static string _buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string _testDataRoot = Path.Combine(_buildDir, "../../..", "data");
        
        public FFMpegProgressLogTests()
        {

        }

        [Fact]
        public async Task Should_Return_Correct_OutTimeMs()
        {
            string filePath = Path.Combine(_testDataRoot, "complete_progress.txt");
            List<FFMpegProgressLogEntry> entries = await FFMpegProgressLog.ParseLogFileAsync(filePath);
            FFMpegProgressLogEntry log = entries[entries.Count - 1];

            double outTime = log.OutTime.TotalMilliseconds;

            Assert.Equal(4204, outTime);
        }

        [Fact]
        public async Task Should_Return_Correct_CurrentFrame()
        {
            string filePath = Path.Combine(_testDataRoot, "complete_progress.txt");
            List<FFMpegProgressLogEntry> entries = await FFMpegProgressLog.ParseLogFileAsync(filePath);
            FFMpegProgressLogEntry log = entries[entries.Count - 1];

            long frame = log.CurrentFrame;

            Assert.Equal(996, frame);
        }

    }
}