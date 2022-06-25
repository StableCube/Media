using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CliWrap;
using CliWrap.Buffered;
using StableCube.Media.FFProbe;
using StableCube.Media.FFMpeg.CommandBuilder;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Generic;
using StableCube.Media.FFMpeg.DataModel;

namespace StableCube.Media.FFMpeg
{
    public class FFMpegService : IFFMpegService
    {
        private IFFProbeService _ffProbe;
        public const string BinaryName = "ffmpeg";
        public string BinaryRoot { get; private set; }
        public string BinaryPath { get; private set; }

        public FFMpegService()
        {
            string pathVar = Environment.GetEnvironmentVariable("PATH");

            if(pathVar != null && BinaryPath == null)
            {
                var binPaths = pathVar.Split(':');
                foreach (var binPath in binPaths)
                {
                    string path = Path.Combine(binPath, BinaryName);
                    if(File.Exists(path))
                    {
                        BinaryRoot = binPath;
                        BinaryPath = path;
                        break;
                    }
                }
            }

            if(BinaryPath == null)
                throw new InvalidProgramException($"Could not find binary '{BinaryName}' in '{pathVar}'. Is FFMpeg installed?");

            _ffProbe = new FFProbeService(Path.Combine(BinaryRoot, FFProbeService.BinaryName));
        }

        public FFMpegService(string ffmpegBinaryPath)
        {
            if(!File.Exists(ffmpegBinaryPath))
                throw new FileNotFoundException(ffmpegBinaryPath);
            
            BinaryPath = ffmpegBinaryPath;
            var info = new FileInfo(ffmpegBinaryPath);

            _ffProbe = new FFProbeService(Path.Combine(info.DirectoryName, FFProbeService.BinaryName));
        }

        public FFMpegService(string ffmpegBinaryPath, string ffprobeBinaryPath)
        {
            if(!File.Exists(ffmpegBinaryPath))
                throw new FileNotFoundException(ffmpegBinaryPath);
            
            if(!File.Exists(ffprobeBinaryPath))
                throw new FileNotFoundException(ffprobeBinaryPath);

            BinaryPath = ffmpegBinaryPath;

            _ffProbe = new FFProbeService(ffprobeBinaryPath);
        }

        public async Task<FFMpegCommandResult> RunCommandTaskAsync(
            FFMpegCommandTask commandTask,
            FFMpegLogOptions logOptions = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // Add options needed to ensure the command is processed correctly
            CommandTaskBuilder cmdBuilder = new CommandTaskBuilder(commandTask);
            CommandOptionGroupBuilder optGroupBuilder = cmdBuilder.BuildOptionGroup(0);
            optGroupBuilder.GlobalScope.GenericOptions
                .AddLogLevel(FFMpegLogLevel.Fatal)
                .AddOverwriteOutputYes();

            if(logOptions != null)
            {
                optGroupBuilder.GlobalScope.MainOptions
                    .AddProgress(logOptions.LogFilePath);
            }

            return await RunCommandTaskAsync(
                commandTask.ToString(),
                logOptions,
                cancellationToken
            );
        }

        public async Task<FFMpegCommandResult> RunCommandTaskAsync(
            string command,
            FFMpegLogOptions logOptions = null,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            CommandTask<BufferedCommandResult> transcodeTask = Cli.Wrap(BinaryPath)
                .WithArguments(command)
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync(cancellationToken);

            BufferedCommandResult result;
            if(logOptions != null)
            {
                Task progressTask = ProgressMonitorAsync(
                    logFilePath: logOptions.LogFilePath, 
                    sourceFilePath: logOptions.SourceFilePath, 
                    progressData: logOptions.ProgressData, 
                    ffmpegTask: transcodeTask,
                    cancellationToken: cancellationToken
                );

                FFMpegProgressMonitorException timeoutEx = null;
                try
                {
                    await Task.WhenAll(transcodeTask, progressTask);
                }
                catch (FFMpegProgressMonitorException e)
                {
                    timeoutEx = e;
                }

                result = transcodeTask.Task.Result;
                if(timeoutEx != null && String.IsNullOrEmpty(result.StandardError))
                {
                    result = new BufferedCommandResult(
                        1, 
                        DateTimeOffset.UtcNow, 
                        DateTimeOffset.UtcNow, 
                        null, 
                        timeoutEx.Message
                    );
                }
            }
            else
            {
                result = await transcodeTask;
            }

            return new FFMpegCommandResult(result);
        }

        private async Task ProgressMonitorAsync(
            string logFilePath, 
            string sourceFilePath, 
            IProgress<ProgressResult> progressData,
            CommandTask<BufferedCommandResult> ffmpegTask,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            int lastProgress = -1;
            int logExistsTimeoutCount = 0;
            int logExistsTimeoutMax = 100;
            int logParseTimeoutCount = 0;
            int logParseTimeoutMax = 100;
            bool isFFMpegRunning = true;

            TimeSpan? duration = await _ffProbe.DurationAsync(
                filePath: sourceFilePath,
                cancellationToken: cancellationToken
            );

            do
            {
                logExistsTimeoutCount++;

                if(cancellationToken.IsCancellationRequested)
                    return;

                if(logExistsTimeoutCount > logExistsTimeoutMax)
                {
                    throw new FFMpegProgressMonitorException("Timeout while waiting for the FFMpeg progress log to appear");
                }

                await Task.Delay(500);
            }
            while(!System.IO.File.Exists(logFilePath));

            do
            {
                if(cancellationToken.IsCancellationRequested)
                    return;

                if(ffmpegTask.Task.Status == System.Threading.Tasks.TaskStatus.Faulted)
                {
                    throw new FFMpegProgressMonitorException("FFMpeg thread faulted while monitoring progress.", ffmpegTask.Task.Exception);
                }
                
                List<FFMpegProgressLogEntry> entries = await FFMpegProgressLog.ParseLogFileAsync(logFilePath);
                if(entries.Count < 1)
                {
                    logParseTimeoutCount++;

                    if(logParseTimeoutCount > logParseTimeoutMax)
                    {
                        throw new FFMpegProgressMonitorException("Timeout while trying to parse log file");
                    }

                    await Task.Delay(500);
                    continue;
                }

                FFMpegProgressLogEntry log = entries[entries.Count - 1];
                ProgressResult progressResult = new ProgressResult(0, 0);

                if(log.Streams.Count > 1)
                {
                    int completeCount = 0;
                    foreach (var stream in log.Streams)
                    {
                        if(stream.Value != 0)
                            completeCount++;
                    }

                    progressResult = new ProgressResult(
                        (int)Math.Floor((double)(((double)completeCount / (double)log.Streams.Count) * 100)),
                        log.Speed);
                }
                else
                {
                    if(duration.HasValue)
                    {
                        progressResult = new ProgressResult(
                            duration.Value, 
                            log.OutTime,
                            log.Speed);
                    }
                }

                if(progressResult.Progress != lastProgress)
                {
                    progressData?.Report(progressResult);
                    lastProgress = progressResult.Progress;
                }

                isFFMpegRunning = ((ffmpegTask.Task.Status == System.Threading.Tasks.TaskStatus.Running) 
                    || (ffmpegTask.Task.Status == System.Threading.Tasks.TaskStatus.WaitingForActivation)
                    || (ffmpegTask.Task.Status == System.Threading.Tasks.TaskStatus.WaitingToRun));

                await Task.Delay(500);
            }
            while(isFFMpegRunning);

            // Make sure to send a final progress report to keep things tidy
            if(lastProgress != 100)
                progressData?.Report(new ProgressResult(100, 0));
        }
    }
}