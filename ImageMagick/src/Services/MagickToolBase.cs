using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using StableCube.Media.ImageMagick.CommandBuilder;

namespace StableCube.Media.ImageMagick
{
    public abstract class MagickToolBase : IMagickTool
    {
        public static string BinaryPath { get; set; }
        public static string BinaryName { get { return "magick"; } }
        private string _toolName;

        public MagickToolBase(string toolName)
        {
            _toolName = toolName;

            string pathVar = Environment.GetEnvironmentVariable("PATH");

            if(BinaryPath == null)
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
            {
                throw new InvalidProgramException($"Could not find binary {BinaryName} in {pathVar}. Is ImageMagick installed?");
            }
        }

        public MagickToolBase(string binaryPath, string toolName)
        {
            _toolName = toolName;
            BinaryPath = binaryPath;
        }

        public async Task<ImageMagickCommandResult> RunCommandAsync(
            ImageMagickCommand cmd,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            foreach (var inputFile in cmd.InputFiles)
            {
                string sourcePath = inputFile.Value.FilePath;
                if(!File.Exists(sourcePath))
                    throw new FileNotFoundException(sourcePath); 
            }

            string cmdStr = $"{_toolName} " + cmd.ToString();

            var cmdResult = await Cli.Wrap(BinaryPath)
                .WithArguments(cmdStr)
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync(cancellationToken);

            return new ImageMagickCommandResult(cmdResult);
        }

        public async Task<string> GetVersionAsync(
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            var cmdResult = await Cli.Wrap(BinaryPath)
                .WithArguments("-version")
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync(cancellationToken);

            return Regex.Match(cmdResult.StandardOutput, @"ImageMagick (.+?) http").Groups[1].Value;
        }
    }
}