
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Probe
{
    /// <summary>
    ///     Count the number of frames per stream and report it in the corresponding stream section.
    /// </summary>
    public class CountFramesOption : ProbeOptionBase
    {
        public const string Key = "-count_frames";

        public CountFramesOption() : base(Key) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, null);
        }
    }
}