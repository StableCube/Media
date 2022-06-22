using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;

namespace StableCube.Media.WebP
{
    public class Gif2WebPService : IGif2WebPService
    {
        public static string BinaryName { get { return "gif2webp"; } }

        public static string BinaryPath { get; private set; }

        public Gif2WebPService()
        {
            ScanForBinary();
        }

        public Gif2WebPService(string binaryPath)
        {
            BinaryPath = binaryPath;
        }

        private void ScanForBinary()
        {
            string pathVar = Environment.GetEnvironmentVariable("PATH");
            if(BinaryPath == null && pathVar != null)
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
                throw new InvalidProgramException($"Could not find binary ${BinaryName} in ${pathVar}. Is webp installed?");
        }

        public async Task<WebPCommandResult> RunAsync(
            string sourceFilePath,
            Gif2WebPOptions options,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            var command = new StringBuilder();
            
            foreach (var item in options.GetCommandArguments())
            {
                if(item.Value != null)
                {
                    command.Append(String.Format(" {0} {1}", item.Key, item.Value));
                }
                else
                {
                    command.Append(String.Format(" {0}", item.Key));
                }
            }

            command.Append(String.Format(" {0}", sourceFilePath));

            var cmdResult = await Cli.Wrap(BinaryPath)
                .WithArguments(command.ToString())
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync(cancellationToken);

            return new WebPCommandResult(cmdResult);
        }
    }
}