using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using CliWrap;
using CliWrap.Buffered;
using StableCube.Media.FFMpeg.CommandBuilder;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Generic;
using StableCube.Media.FFProbe.DataModel;

namespace StableCube.Media.FFProbe
{
    public class FFProbeService : IFFProbeService
    {
        public const string BinaryName = "ffprobe";

        public static string BinaryPath { get; private set; }

        private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { 
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters = {
                new StringBooleanConverter(),
            }
        };

        public FFProbeService()
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
                        BinaryPath = path;
                        break;
                    }
                }
            }

            if(BinaryPath == null)
                throw new InvalidProgramException($"Could not find binary ${BinaryName} in ${pathVar}. Is FFProbe installed?");
        }

        public FFProbeService(string binaryPath)
        {
            if(!File.Exists(binaryPath))
                throw new FileNotFoundException(binaryPath);

            BinaryPath = binaryPath;
        }

        public async Task<FFProbeFileInfo> RunCommandAsync(
            FFMpegCommandTask commandTask,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            CommandTaskBuilder optBuilder = new CommandTaskBuilder(commandTask);
            optBuilder.BuildOptionGroup(0).GlobalScope.GenericOptions
            .AddHideBanner()
            .AddLogLevel(FFMpegLogLevel.Fatal);

            string command = commandTask.ToString();
            FFProbeFileInfo result = null;

            var cmd = await Cli.Wrap(BinaryPath)
                .WithArguments(command)
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync(cancellationToken);

            if(cmd.ExitCode != 0)
            {
                string errorMsg = "FFProbe Command failed with: " + cmd.StandardError;
                throw new FFProbeProcessException(errorMsg);
            }

            result = JsonSerializer.Deserialize<FFProbeFileInfo>(cmd.StandardOutput, _serializerOptions);

            return result;
        }

        /// <summary>
        /// A full ffprobe report on a file
        /// </summary>
        public async Task<FFProbeFileInfo> FileInfoAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            if(!filePath.StartsWith("http") && !File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            CommandTaskBuilder optBuilder = new CommandTaskBuilder();
            var groupOpts = optBuilder.BuildOptionGroup(0);
            var inputOpts = groupOpts.AddInput(0, filePath);

            groupOpts.OutputScope.ProbeOptions
            .AddOutputFormat("json")
            .AddShowFormat()
            .AddShowStreams();

            FFProbeFileInfo result  = await RunCommandAsync(
                commandTask: optBuilder.Task,
                cancellationToken: cancellationToken
            );

            if(result.Streams == null)
                throw new FFProbeProcessException($"No streams found in {filePath}");

            return result;
        }

        /// <summary>
        /// Returns just the format info
        /// </summary>
        public async Task<FFProbeFormat> GetFormatAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            if(!filePath.StartsWith("http") && !File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            CommandTaskBuilder optBuilder = new CommandTaskBuilder();
            var groupOpts = optBuilder.BuildOptionGroup(0);
            var inputOpts = groupOpts.AddInput(0, filePath);

            groupOpts.OutputScope.ProbeOptions
            .AddOutputFormat("json")
            .AddShowFormat();

            var result = await RunCommandAsync(
                commandTask: optBuilder.Task,
                cancellationToken: cancellationToken
            );

            return result.Format;
        }

        /// <summary>
        /// Finds the index of the first video stream
        /// </summary>
        public async Task<int?> GetVideoStreamIndexAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            if(!filePath.StartsWith("http") && !File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            CommandTaskBuilder optBuilder = new CommandTaskBuilder();
            var groupOpts = optBuilder.BuildOptionGroup(0);
            var inputOpts = groupOpts.AddInput(0, filePath);

            groupOpts.OutputScope.ProbeOptions
            .AddOutputFormat("json")
            .ProbeOptions
            .AddShowEntries("stream=index:stream=codec_type");

            var info = await RunCommandAsync(
                commandTask: optBuilder.Task,
                cancellationToken: cancellationToken
            );

            var videoStreams = info.GetVideoStreams();

            if(videoStreams.Count < 1)
                return null;

            return videoStreams[0].Index;
        }

        /// <summary>
        /// Fast total frame count but does not work on all formats
        /// </summary>
        public async Task<long?> FrameCountFastAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            if(!filePath.StartsWith("http") && !File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            CommandTaskBuilder optBuilder = new CommandTaskBuilder();
            var groupOpts = optBuilder.BuildOptionGroup(0);
            var inputOpts = groupOpts.AddInput(0, filePath);

            groupOpts.OutputScope.ProbeOptions
            .AddOutputFormat("json")
            .AddShowEntries("stream=nb_frames")
            .AddSelectStreams(new StreamSpecifier('v', streamIndex));

            var result = await RunCommandAsync(
                commandTask: optBuilder.Task,
                cancellationToken: cancellationToken
            );

            if(result.Streams.Length < 1)
                return null;

            long frameCount = result.Streams[0].TotalFrames;
            if(frameCount < 1)
                return null;

            return frameCount;
        }

        /// <summary>
        /// Much more accurate frame count but is quite slow
        /// </summary>
        public async Task<long?> FrameCountThoroughAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            if(!filePath.StartsWith("http") && !File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            CommandTaskBuilder optBuilder = new CommandTaskBuilder();
            var groupOpts = optBuilder.BuildOptionGroup(0);
            var inputOpts = groupOpts.AddInput(0, filePath);

            groupOpts.OutputScope.ProbeOptions
            .AddOutputFormat("json")
            .AddShowEntries("stream=nb_read_frames")
            .AddSelectStreams(new StreamSpecifier('v', streamIndex))
            .AddShowFormat()
            .AddShowStreams()
            .AddCountFrames();

            var result = await RunCommandAsync(
                commandTask: optBuilder.Task,
                cancellationToken: cancellationToken
            );

            if(result.Streams.Length < 1)
                return null;

            long frameCount = result.Streams[0].TotalReadFrames;
            if(frameCount < 1)
                return null;

            return frameCount;
        }

        /// <summary>
        /// Attempts a fast frame count and falls back on thorough if failed
        /// </summary>
        public async Task<long?> FrameCountSmartAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            if(!filePath.StartsWith("http") && !File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            long? count = await FrameCountFastAsync(
                filePath: filePath, 
                streamIndex: streamIndex,
                cancellationToken: cancellationToken
            );

            if(count.HasValue)
                return count.Value;

            count = await FrameCountThoroughAsync(
                filePath: filePath, 
                streamIndex: streamIndex,
                cancellationToken: cancellationToken
            );

            if(count.HasValue)
                return count.Value;

            return null;
        }

        /// <summary>
        /// Finds the duration
        /// </summary>
        public async Task<TimeSpan?> DurationAsync(
            string filePath, 
            int streamIndex = 0,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            if(!filePath.StartsWith("http") && !File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            CommandTaskBuilder optBuilder = new CommandTaskBuilder();
            var groupOpts = optBuilder.BuildOptionGroup(0);
            var inputOpts = groupOpts.AddInput(0, filePath);

            groupOpts.OutputScope.ProbeOptions
            .AddOutputFormat("json")
            .AddShowEntries("stream=duration")
            .AddSelectStreams(new StreamSpecifier('v', streamIndex))
            .AddShowFormat();

            var result = await RunCommandAsync(
                commandTask: optBuilder.Task,
                cancellationToken: cancellationToken
            );

            float duration = result.Format.Duration;
            if(duration > 0)
                return TimeSpan.FromSeconds(duration);

            if(result.Streams.Length > 0)
            {
                duration = result.Streams[0].Duration;
                if(duration > 0)
                    return TimeSpan.FromSeconds(duration);
            }

            return null;
        }

        /// <summary>
        /// Returns list of formats for a file
        /// </summary>
        public async Task<List<VideoFormat>> GetFormatsAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            if(!filePath.StartsWith("http") && !File.Exists(filePath))
                throw new FileNotFoundException(filePath);
            
            FFProbeFormat formatInfo = await GetFormatAsync(
                filePath: filePath,
                cancellationToken: cancellationToken
            );

            return FFProbeFormatToVideoFormats(formatInfo);
        }

        private List<VideoFormat> FFProbeFormatToVideoFormats(FFProbeFormat formatInfo)
        {
            List<VideoFormat> formatList = new List<VideoFormat>();

            //Sometimes the format is a comma delimiated list
            string[] list = formatInfo.FormatName.Split(',');
            if(list.Length > 0)
            {
                foreach(string formatName in list)
                    formatList.Add(FormatStringToEnum(formatName));
            }

            return formatList;
        } 

        private static VideoFormat FormatStringToEnum(string format)
        {
            if(format == "3gp")
                return VideoFormat._3gp;

            if(format == "3g2")
                return VideoFormat._3g2;

            foreach (var formatEnum in (VideoFormat[])Enum.GetValues(typeof(VideoFormat)))
            {
                if(format == formatEnum.ToString().ToLower())
                    return formatEnum;
            }

            return VideoFormat.Unknown;
        }

        /// <summary>
        /// Returns info on if a file is a valid video
        /// </summary>
        public async Task<FileTypeInfo> GetFileTypeInfoAsync(
            string filePath,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            if(!filePath.StartsWith("http") && !File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            CommandTaskBuilder optBuilder = new CommandTaskBuilder();
            var groupOpts = optBuilder.BuildOptionGroup(0);
            var inputOpts = groupOpts.AddInput(0, filePath);

            groupOpts.OutputScope.ProbeOptions
            .AddOutputFormat("json")
            .AddShowEntries("stream=codec_type:stream=codec_name")
            .AddShowFormat();

            FFProbeFileInfo info;
            try
            {
                info = await RunCommandAsync(
                    commandTask: optBuilder.Task,
                    cancellationToken: cancellationToken
                );
            }
            catch (FFProbeProcessException)
            {
                return new FileTypeInfo()
                {
                    IsVideo = false,
                };
            }

            var videoStreams = info.GetVideoStreams();
            string codecName = String.Empty;
            string codecType = String.Empty;
            bool isVideo = false;
            var formats = FFProbeFormatToVideoFormats(info.Format);

            if(videoStreams.Count > 0)
            {
                string streamCodecName = videoStreams[0].CodecName;
                string streamCodecType = videoStreams[0].CodecType;

                if(streamCodecName != null && streamCodecType != null)
                {
                    codecName = streamCodecName;
                    codecType = streamCodecType;

                    if(streamCodecType == "video" && !formats.Contains(VideoFormat.Unknown))
                        isVideo = true;
                }
            }

            return new FileTypeInfo()
            {
                CodecName = codecName,
                CodecType = codecType,
                IsVideo = isVideo,
                Format = formats
            };
        }
    }
}