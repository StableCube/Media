
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Probe
{
    /// <summary>
    ///     Show information about the container format of the input multimedia stream.
    ///     All the container format information is printed within a section with name "FORMAT".
    /// </summary>
    public class ShowFormatOption : ProbeOptionBase
    {
        public const string Key = "-show_format";

        public ShowFormatOption() : base(Key) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(Key, null);
        }
    }
}