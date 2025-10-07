
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class FilterOption : MainOptionBase
    {
        public const string Key = "-filter";

        /// <summary>
        ///     Create the filtergraph specified by filtergraph and use it to filter the stream.
        ///     filtergraph is a description of the filtergraph to apply to the stream, and must have a single input 
        ///     and a single output of the same type of the stream. In the filtergraph, the input is associated to the 
        ///     label in, and the output to the label out. See the ffmpeg-filters manual for more information about the 
        ///     filtergraph syntax. 
        /// <summary>
        public FilterGraph FilterGraph { get; set; }

        public FilterOption() : base(Key, null) {}

        public FilterOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, FilterGraph.ToString());
        }
    }
}