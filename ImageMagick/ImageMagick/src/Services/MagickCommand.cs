using System;
using System.Threading;
using System.Threading.Tasks;
using StableCube.Media.ImageMagick.CommandBuilder;
using StableCube.Media.ImageMagick.DataModel;

namespace StableCube.Media.ImageMagick
{
    public class MagickCommand
    {
        public static async Task<ImageMagickCommandResult> ConvertAsync(
            ImageMagickCommand cmd,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            var service = new MagickConvert();

            ImageMagickCommandResult cmdResult = await service.RunCommandAsync(
                cmd: cmd,
                cancellationToken: cancellationToken
            );

            return cmdResult;
        }

        public static async Task<ImageInfo> ProbeAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            var service = new MagickConvert();
            var infoResult = await service.GetInfoAsync(
                filePath: filePath,
                cancellationToken: cancellationToken
            );

            if(infoResult.ExitCode != 0)
                throw new InfoProbeException(infoResult.StandardError);

            return infoResult.ImageInfo;
        }
    }
}
