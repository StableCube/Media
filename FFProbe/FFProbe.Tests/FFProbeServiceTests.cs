using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace StableCube.Media.FFProbe.Tests
{
    public class FFProbeServiceTests
    {
        private static string _projectRoot = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../..");
        private static string _testMediaRoot = Path.Combine(_projectRoot, "TestMedia");
        private static string _testFilePath = Path.Combine(_testMediaRoot, "h264_720x480.mp4");

        public FFProbeServiceTests()
        {
        }

        [Fact]
        public async Task Should_Probe_File_Format()
        {
            FFProbeService probe = new FFProbeService();
            var result = await probe.GetFormatAsync(_testFilePath);

            Assert.Contains("mp4", result.FormatName);
        }

        [Fact]
        public async Task Should_Probe_File_Is_Video_True()
        {
            FFProbeService probe = new FFProbeService();
            var result = await probe.GetFileTypeInfoAsync(_testFilePath);

            Assert.True(result.IsVideo);
        }

        [Fact]
        public async Task Should_Probe_File_Is_Video_False()
        {
            FFProbeService probe = new FFProbeService();
            var result = await probe.GetFileTypeInfoAsync(Path.Combine(_testMediaRoot, "not_video.txt"));

            Assert.False(result.IsVideo);
        }

        [Fact]
        public async Task Should_Probe_Filename()
        {
            FFProbeService probe = new FFProbeService();
            var result = await probe.FileInfoAsync(_testFilePath);

            Assert.Equal(_testFilePath, result.Format.FileName);
        }

        [Fact]
        public async Task Should_Probe_Frame_Count_Smart()
        {
            FFProbeService probe = new FFProbeService();
            var result = await probe.FrameCountSmartAsync(_testFilePath, 0);

            Assert.Equal(774, result.Value);
        }

        [Fact]
        public async Task Should_Get_Duration_Seconds()
        {
            FFProbeService probe = new FFProbeService();
            var result = await probe.DurationAsync(_testFilePath, 0);

            Assert.Equal(30, (int)result.Value.TotalSeconds);
        }
    }
}