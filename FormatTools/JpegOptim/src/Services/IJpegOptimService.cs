using System.Threading;
using System.Threading.Tasks;

namespace StableCube.Media.JpegOptim
{
    public interface IJpegOptimService
    {
        Task<JpegOptimCommandResult> RunAsync(
            string sourceFilePath,
            JpegOptimOptions options,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}