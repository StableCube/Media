using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class FilterComplexOption : MainOptionBase
    {
        public const string Key = "-filter_complex";

        /// <summary>
        /// Define a complex filtergraph, i.e. one with arbitrary number of inputs and/or outputs. 
        /// For simple graphs – those with one input and one output of the same type – see the -filter options. 
        /// filtergraph is a description of the filtergraph, as described in the “Filtergraph syntax” section of the ffmpeg-filters manual. 
        /// </summary>
        /// <value></value>
        public FilterGraph Filter { get; set; } = new FilterGraph();

        public FilterComplexOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            string filterString = Filter.ToString();
            if(filterString == String.Empty)
                return new CommandParam(null, null);

            return new CommandParam(OptionKey, $"\"{Filter.ToString()}\"");
        }
    }
}