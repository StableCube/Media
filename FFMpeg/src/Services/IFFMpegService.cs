using System.Threading;
using System.Threading.Tasks;
using StableCube.Media.FFMpeg.CommandBuilder;
using StableCube.Media.FFMpeg.DataModel;

namespace StableCube.Media.FFMpeg
{
    public interface IFFMpegService
    {
        Task<FFMpegCommandResult> RunCommandTaskAsync( 
            FFMpegCommandTask commandTask,
            FFMpegLogOptions logOptions = null,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        Task<FFMpegCommandResult> RunCommandTaskAsync( 
            string command,
            FFMpegLogOptions logOptions = null,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}