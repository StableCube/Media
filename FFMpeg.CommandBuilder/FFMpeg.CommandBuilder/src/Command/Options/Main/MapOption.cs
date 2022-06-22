
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class MapOption : MainOptionBase
    {
        public const string Key = "-map";

        /// <summary>
        /// Designate one or more input streams as a source for the output file. Each input stream is identified by the 
        /// input file index input_file_id and the input stream index input_stream_id within the input file. Both indices start at 0. 
        /// If specified, sync_file_id:stream_specifier sets which input stream is used as a presentation sync reference. 
        /// </summary>
        /// <url>https://ffmpeg.org/ffmpeg.html#toc-Advanced-options</url>
        /// <value></value>
        public string Map { get; set; }

        public MapOption() : base (Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, Map);
        }
    }
}