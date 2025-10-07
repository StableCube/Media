
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Probe
{
    /// <summary>
    /// Set the output printing format. 
    /// </summary>
    /// <example>-print_format json</example>
    public class OutputFormatOption : ProbeOptionBase
    {
        public const string Key = "-of";

        public string Format { get; set; }

        public OutputFormatOption() : base(Key) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, Format);
        }
    }
}