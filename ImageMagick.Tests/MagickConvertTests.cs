using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using StableCube.Media.ImageMagick.CommandBuilder;

namespace StableCube.Media.ImageMagick.Tests
{
    public class MagickConvertTests : IDisposable
    {
        private static string _projectRoot = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../..");
        private static string _testMediaRoot = Path.Combine(_projectRoot, "TestMedia");
        private static string _animatedGifPath = Path.Combine(_testMediaRoot, "animated.gif");
        private static string _pngTransparentPath = Path.Combine(_testMediaRoot, "with_transparency.png");
        private static string _pngPath = Path.Combine(_testMediaRoot, "kitten.png");
        private static string _watermarkPath = Path.Combine(_testMediaRoot, "watermark.png");
        private string _outPath = "/tmp/imagemagick_tests/";
        private IMagickConvert _service;

        public MagickConvertTests()
        {
            if(!Directory.Exists(_outPath))
                Directory.CreateDirectory(_outPath);

            _service = new MagickConvert();
        }

        public void Dispose()
        {
            if(Directory.Exists(_outPath))
               Directory.Delete(_outPath, true);
        }

        [Fact]
        public async Task Should_Return_Version()
        {
            var result = await _service.GetVersionAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Convert_Png_To_Jpeg()
        {
            string sourceFilePath = _pngTransparentPath;
            string outputFilePath = Path.Combine(_outPath, "out.jpg");

            var cmd = new ImageMagickCommand();
            cmd.AddInputFile(new InputFile(sourceFilePath));
            cmd.OutputFilePath = outputFilePath;

            var result = await _service.RunCommandAsync(cmd);

            Assert.True(File.Exists(outputFilePath));
            Assert.Equal(0, result.ExitCode);
        }

        [Fact]
        public async Task Should_Create_Image_With_Watermark_Overlay()
        {
            string sourceFilePath = _pngPath;
            string watermarkFilePath = _watermarkPath;
            string outputFilePath = Path.Combine(_outPath, "out.jpg");

            var imgFile = new InputFile(sourceFilePath);
            var watermarkInputFile = new InputFile(watermarkFilePath);

            watermarkInputFile.OptionSequence.Add(new ComposeOption()
            {
                Compose = new CompositionMethod(ComposeType.Multiply)
            });

            watermarkInputFile.OptionSequence.Add(new GravityOption()
            {
                Gravity = GravityOption.GravityType.South
            });

            watermarkInputFile.OptionSequence.Add(new CompositeOption()
            {
                Alternate = true
            });

            var cmd = new ImageMagickCommand(outputFilePath);
            cmd.AddInputFile(imgFile, 0);
            cmd.AddInputFile(watermarkInputFile, 1);
            
            var result = await _service.RunCommandAsync(cmd);

            Assert.True(File.Exists(outputFilePath));
            Assert.Equal(0, result.ExitCode);
        }

        [Fact]
        public async Task Should_Identify_Png()
        {
            string sourceFilePath = _pngPath;

            var result = await _service.GetInfoAsync(sourceFilePath);

            Assert.Equal("image/png", result.ImageInfo.MimeType);
        }

        [Fact]
        public async Task Should_Identify_Animated_Gif()
        {
            string sourceFilePath = _animatedGifPath;

            var result = await _service.GetInfoAsync(sourceFilePath);

            Assert.Equal("image/gif", result.ImageInfo.MimeType);
            Assert.Equal(12, result.ImageInfo.Frames.Length);
        }
    }
}