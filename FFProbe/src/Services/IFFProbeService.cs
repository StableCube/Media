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
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// A full ffprobe report on a file
        /// </summary>
        Task<FFProbeFileInfo> FileInfoAsync(
            string filePath,
            CancellationToken cancellationToken = default
        );

        Task<FFProbeFormat> GetFormatAsync(
            string filePath,
            CancellationToken cancellationToken = default
        );

        Task<int?> GetVideoStreamIndexAsync(
            string filePath,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Fast total frame count but does not work on all formats
        /// </summary>
        Task<long?> FrameCountFastAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Much more accurate frame count but is quite slow
        /// </summary>
        Task<long?> FrameCountThoroughAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default
        );

        Task<long?> FrameCountSmartAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default
        );

        Task<TimeSpan?> DurationAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default
        );

        Task<FileTypeInfo> GetFileTypeInfoAsync(
            string filePath,
            CancellationToken cancellationToken = default
        );
    }
}