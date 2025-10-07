using System.Threading;
using System.Threading.Tasks;

namespace StableCube.Media.WebP
{
    public interface ICWebPService
    {
        Task<WebPCommandResult> RunAsync(
            string sourceFilePath,
            CWebPOptions options,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}