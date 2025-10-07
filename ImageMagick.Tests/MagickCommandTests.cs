using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using StableCube.Media.ImageMagick.DataModel;

namespace StableCube.Media.ImageMagick.Tests
{
    public class MagickCommandTests
    {
        private static string _projectRoot = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../..");
        private static string _testMediaRoot = Path.Combine(_projectRoot, "TestMedia");
        private static string _animatedGifPath = Path.Combine(_testMediaRoot, "animated.gif");
        private static string _pngTransparentPath = Path.Combine(_testMediaRoot, "with_transparency.png");
        private static string _pngPath = Path.Combine(_testMediaRoot, "kitten.png");

        public MagickCommandTests()
        {
        }

        [Fact]
        public async Task Should_Identify_Image_As_Png()
        {
            string sourceFilePath = _pngPath;

            var result = await MagickCommand.ProbeAsync(sourceFilePath);

            Assert.Equal(ImageType.Png, result.ImageType);
        }

        [Fact]
        public async Task Should_Identify_Image_As_Gif()
        {
            string sourceFilePath = _animatedGifPath;

            var result = await MagickCommand.ProbeAsync(sourceFilePath);

            Assert.Equal(ImageType.Gif, result.ImageType);
        }

        [Fact]
        public async Task Should_Read_File_Width()
        {
            string sourceFilePath = _animatedGifPath;

            var result = await MagickCommand.ProbeAsync(sourceFilePath);

            Assert.Equal(350, result.Geometry.Width);
        }

        [Fact]
        public async Task Should_Read_File_Has_Transparency()
        {
            string sourceFilePath = _pngTransparentPath;

            var result = await MagickCommand.ProbeAsync(sourceFilePath);

            Assert.True(result.HasTransparency);
        }

        [Fact]
        public async Task Should_Read_File_No_Transparency()
        {
            string sourceFilePath = _pngPath;

            var result = await MagickCommand.ProbeAsync(sourceFilePath);

            Assert.False(result.HasTransparency);
        }

        [Fact]
        public async Task Should_Read_File_Has_Animation()
        {
            string sourceFilePath = _animatedGifPath;

            var result = await MagickCommand.ProbeAsync(sourceFilePath);

            Assert.True(result.IsAnimated);
        }
    }
}