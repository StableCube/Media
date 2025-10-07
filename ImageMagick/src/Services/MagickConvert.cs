using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;

namespace StableCube.Media.ImageMagick
{
    public class MagickConvert : MagickToolBase, IMagickConvert
    {
        public static string ToolName { get { return "convert"; } }

        public MagickConvert() : base(ToolName)
        {
        }

        public MagickConvert(string magickBinaryPath) : base(magickBinaryPath, ToolName)
        {
        }

        public async Task<ImageMagickCommandResult> GetInfoJsonAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(!File.Exists(filePath))
                throw new FileNotFoundException(filePath); 

            string cmdStr = $"convert {filePath} json:-";

            var cmdResult = await Cli.Wrap(BinaryPath)
                .WithArguments(cmdStr)
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync(cancellationToken);

            return new ImageMagickCommandResult(cmdResult);
        }

        public async Task<ImageMagickProbeResult> GetInfoAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(!File.Exists(filePath))
                throw new FileNotFoundException(filePath); 

            ImageMagickProbeResult cmdResult = null;
            var jsonResult = await GetInfoJsonAsync(filePath, cancellationToken);
            if(jsonResult.ExitCode != 0)
                throw new InfoProbeException(jsonResult.StandardError);

            var imgInfo = ImageInfoSerialization.FromJson(jsonResult.StandardOutput, filePath);
            cmdResult = new ImageMagickProbeResult(jsonResult, imgInfo);

            return cmdResult;
        }
    }
}