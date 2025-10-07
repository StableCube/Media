using System.Threading;
using System.Threading.Tasks;

namespace StableCube.Media.PngQuant
{
    public interface IPngQuantService
    {
        Task<PngQuantCommandResult> RunAsync(
            string filePath, 
            PngQuantOptions options,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}