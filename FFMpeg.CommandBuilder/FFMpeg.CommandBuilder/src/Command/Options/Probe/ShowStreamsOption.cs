
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Probe
{
    /// <summary>
    ///     Show information about each media stream contained in the input multimedia stream.
    /// 
    ///     Each media stream information is printed within a dedicated section with name "STREAM".
    /// </summary>
    public class ShowStreamsOption : ProbeOptionBase
    {
        public const string Key = "-show_streams";

        public ShowStreamsOption() : base(Key) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(Key, null);
        }
    }
}