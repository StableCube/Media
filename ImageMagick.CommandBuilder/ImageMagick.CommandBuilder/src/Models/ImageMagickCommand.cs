using System;
using System.Collections.Generic;

namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class ImageMagickCommand
    {
        public SortedList<int, InputFile> InputFiles { get; set; } = new SortedList<int, InputFile>();

        public string OutputFilePath { get; set; }

        /// <summary>
        /// Options for this command
        /// </summary>
        public CommandOptionSequence OptionSequence { get; set; } = new CommandOptionSequence();

        public ImageMagickCommand()
        {
        }

        public ImageMagickCommand(InputFile inputFile, string outputFilePath, int index = 0)
        {
            InputFiles.Add(index, inputFile);
            OutputFilePath = outputFilePath;
        }

        public ImageMagickCommand(string outputFilePath)
        {
            OutputFilePath = outputFilePath;
        }

        public void AddInputFile(InputFile input, int fileIndex = 0)
        {
            InputFiles.Add(fileIndex, input);
        }

        /// <summary>
        /// Combine the all the parameter elements in the correct order
        /// </summary>
        public List<ImageMagickParameter> GetParameters()
        {
            List<ImageMagickParameter> parameters = new List<ImageMagickParameter>();

            foreach (var option in OptionSequence.Options)
            {
                parameters.Add(option.Value.GetParameter());
            }

            foreach (var input in InputFiles)
            {
                string filePath = input.Value.FilePath;
                if(input.Value.FrameSelector != null)
                    filePath += input.Value.FrameSelector;

                parameters.Add(new ImageMagickParameter(filePath, ""));

                foreach (var option in input.Value.OptionSequence.Options)
                {
                    parameters.Add(option.Value.GetParameter());
                }
            }
            
            if(OutputFilePath != null)
                parameters.Add(new ImageMagickParameter("", OutputFilePath));

            return parameters;
        }

        /// <summary>
        /// Get the full command string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            List<ImageMagickParameter> parameters = GetParameters();
            List<string> values = new List<string>();
            foreach (var param in parameters)
            {
                if(param.Key != null)
                    values.Add(param.Key);

                if(param.Value != null)
                    values.Add(param.Value);
            }

            string cmd = String.Join(" ", values);

            return cmd;
        }

        public string GetCommandString()
        {
            return ToString();
        }
    }
}