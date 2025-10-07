
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Probe
{
    /// <summary>
    /// Select only the streams specified by stream_specifier
    /// </summary>
    public class SelectStreamsOption : ProbeOptionBase
    {
        public const string Key = "-select_streams";

        public StreamSpecifier Stream { get; set; }

        public SelectStreamsOption() : base(Key) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, Stream.ToString());
        }
    }
}