using System;
using System.Threading;
using System.Threading.Tasks;
using StableCube.Media.ImageMagick.CommandBuilder;

namespace StableCube.Media.ImageMagick
{
    public interface IMagickTool
    {
        Task<ImageMagickCommandResult> RunCommandAsync(
            ImageMagickCommand cmd,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        Task<string> GetVersionAsync(
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}