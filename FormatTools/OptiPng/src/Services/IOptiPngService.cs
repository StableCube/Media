using System.Threading;
using System.Threading.Tasks;

namespace StableCube.Media.OptiPng
{
    public interface IOptiPngService
    {
        Task<OptiPngCommandResult> RunAsync(
            string sourceFilePath, 
            OptiPngOptions options,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}