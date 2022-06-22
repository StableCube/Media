using System.Collections.Generic;
using StableCube.Media.FFMpeg.CommandBuilder.Options.Main;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// A serializable group of FFMpeg command options. Each command task can potentially contain many nested command groups.
    /// 
    /// FFMpeg Command structure
    /// ffmpeg option_group [[global_options] {[input_file_options] -i input_url} ... {[output_file_options] output_url}] ...
    /// </summary>
    public class FFMpegCommandOptionGroup : ParameterProvider
    {
        public int GroupIndex { get; set; }

        public SortedList<int, FFMpegInput> InputFiles { get; set; } = new SortedList<int, FFMpegInput>();

        public SortedList<int, string> OutputFiles { get; set; } = new SortedList<int, string>();

        public List<IOption> GlobalOptions { get; set; } = new List<IOption>();

        public List<IOption> OutputFileOptions { get; set; } = new List<IOption>();

        public FilterGraph ComplexFilterGraph { get; set; } = new FilterGraph();

        public FFMpegCommandOptionGroup()
        {
        }

        public FFMpegCommandOptionGroup(int index)
        {
            GroupIndex = index;
        }

        public FFMpegCommandOptionGroup AddInputFile(string filePath, int fileIndex = 0)
        {
            var fileInput = new FFMpegInput(filePath);

            InputFiles.Add(fileIndex, fileInput);

            return this;
        }

        public FFMpegCommandOptionGroup AddInputFile(FFMpegInput fileInput, int fileIndex = 0)
        {
            InputFiles.Add(fileIndex, fileInput);

            return this;
        }

        public void AddOutputFile(string filePath, int fileIndex = 0)
        {
            OutputFiles.Add(fileIndex, filePath);
        }

        /// <summary>
        /// Combine the all the parameter elements in the correct order
        /// </summary>
        public override List<CommandParam> GetParameters()
        {
            List<CommandParam> parameters = new List<CommandParam>();

            foreach (var option in GlobalOptions)
                parameters.Add(option.GetCommandParameter());

            foreach (var input in InputFiles)
                parameters.AddRange(input.Value.GetParameters());

            if(ComplexFilterGraph.FilterChains.Count > 0)
                parameters.Add(ComplexFilterGraph.GetCommandParameter());

            foreach (var option in OutputFileOptions)
                parameters.Add(option.GetCommandParameter());

            foreach (var output in OutputFiles)
                parameters.Add(new CommandParam(output.Value, null));

            return parameters;
        }
    }
}