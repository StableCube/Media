using System;
using System.Threading;
using System.Threading.Tasks;
using StableCube.Media.FFMpeg.CommandBuilder;
using StableCube.Media.FFProbe.DataModel;

namespace StableCube.Media.FFProbe
{
    public interface IFFProbeService
    {
        Task<FFProbeFileInfo> RunCommandAsync(
            FFMpegCommandTask commandTask,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        /// <summary>
        /// A full ffprobe report on a file
        /// </summary>
        Task<FFProbeFileInfo> FileInfoAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        Task<FFProbeFormat> GetFormatAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        Task<int?> GetVideoStreamIndexAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        /// <summary>
        /// Fast total frame count but does not work on all formats
        /// </summary>
        Task<long?> FrameCountFastAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        /// <summary>
        /// Much more accurate frame count but is quite slow
        /// </summary>
        Task<long?> FrameCountThoroughAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        Task<long?> FrameCountSmartAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        Task<TimeSpan?> DurationAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default(CancellationToken)
        );

        Task<FileTypeInfo> GetFileTypeInfoAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        );
    }
}