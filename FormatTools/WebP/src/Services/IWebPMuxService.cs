using System.Threading;
using System.Threading.Tasks;

namespace StableCube.Media.WebP
{
    public interface IWebPMuxService
    {
        Task<WebPCommandResult> RunAsync(
            string sourceFilePath,
            WebPMuxOptions options,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}