using System.Threading;
using System.Threading.Tasks;

namespace StableCube.Media.WebP
{
    public interface IGif2WebPService
    {
        Task<WebPCommandResult> RunAsync(
            string sourceFilePath,
            Gif2WebPOptions options,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}