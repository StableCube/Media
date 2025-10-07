using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using StableCube.Media.ImageMagick.DataModel;

namespace StableCube.Media.ImageMagick.Tests
{
    public class ImageInfoDeserializationTests
    {
        private static string _projectRoot = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../..");
        private static string _testMediaRoot = Path.Combine(_projectRoot, "TestMedia");
        private static string _jpegPath = Path.Combine(_testMediaRoot, "goonies.jpg");
        private static string _animatedGifPath = Path.Combine(_testMediaRoot, "animated.gif");
        private static string _bmpPath = Path.Combine(_testMediaRoot, "test.bmp");

        public ImageInfoDeserializationTests()
        {
        }

        private string GetTestImagePath(string filename)
        {
            return Path.Combine(_testMediaRoot, filename);
        }

        [Fact]
        public async Task Should_Deserialize_Jpeg_Info()
        {
            string testFile = _jpegPath;
            var service = new MagickConvert();
            var response = await service.GetInfoJsonAsync(testFile);
            string json = response.StandardOutput;

            ImageInfo result = ImageInfoSerialization.FromJson(json, testFile);

            Assert.Equal("image/jpeg", result.MimeType);
        }

        [Fact]
        public async Task Should_Deserialize_Animated_Gif_Info()
        {
            string testFile = _animatedGifPath;
            var service = new MagickConvert();
            var response = await service.GetInfoJsonAsync(testFile);
            string json = response.StandardOutput;

            ImageInfo result = ImageInfoSerialization.FromJson(json, testFile);

            Assert.Equal("image/gif", result.MimeType);
            Assert.True((result.Frames.Length > 1));
        }

        [Fact]
        public async Task Should_Deserialize_Bitmap_Info()
        {
            string testFile = _bmpPath;
            var service = new MagickConvert();
            var response = await service.GetInfoJsonAsync(testFile);
            string json = response.StandardOutput;

            ImageInfo result = ImageInfoSerialization.FromJson(json, testFile);

            Assert.Equal("image/bmp", result.MimeType);
        }
    }
}