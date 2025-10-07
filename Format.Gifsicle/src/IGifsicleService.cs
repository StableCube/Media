using System.Threading;
using System.Threading.Tasks;

namespace StableCube.Media.Gifsicle
{
    public interface IGifsicleService
    {
        Task<GifsicleCommandResult> RunAsync(
            string sourceFilePath, 
            GifsicleOptions options,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}