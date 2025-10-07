
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Probe
{
    /// <summary>
    ///     Show information about each frame and subtitle contained in the input multimedia stream.
    /// 
    ///     The information for each single frame is printed within a dedicated section with name "FRAME" or "SUBTITLE".
    /// </summary>
    public class ShowFramesOption : ProbeOptionBase
    {
        public const string Key = "-show_frames";

        public ShowFramesOption() : base(Key) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(Key, null);
        }
    }
}