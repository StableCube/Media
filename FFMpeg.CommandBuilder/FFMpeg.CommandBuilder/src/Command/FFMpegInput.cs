using System;
using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Main;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Represents an FFMpeg input command with proceeding options
    /// 
    /// -i filename.mp4 [options]
    /// </summary>
    public class FFMpegInput : ParameterProvider
    {
        public string FilePath { get; set; }

        public List<IOption> Options { get; set; } = new List<IOption>();

        public FFMpegInput()
        {
        }

        public FFMpegInput(string filePath)
        {
            if(filePath == null)
                throw new ArgumentNullException("filePath");

            FilePath = filePath; 
        }

        public override List<CommandParam> GetParameters()
        {
            List<CommandParam> parameters = new List<CommandParam>();

            var input = new InputOption()
            {
                Path = FilePath
            };

            foreach (var option in Options)
            {
                parameters.Add(option.GetCommandParameter());
            }

            parameters.Add(input.GetCommandParameter());

            return parameters;
        }
    }
}