using System;
using System.Threading;
using System.Threading.Tasks;

namespace StableCube.Media.ImageMagick
{
    public interface IMagickConvert : IMagickTool
    {
        Task<ImageMagickCommandResult> GetInfoJsonAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        Task<ImageMagickProbeResult> GetInfoAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}