
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Generic
{
    /// <summary>
    ///     Suppress printing banner.
    /// 
    ///     All FFmpeg tools will normally show a copyright notice, build options and library versions. 
    ///     This option can be used to suppress printing this information.
    /// </summary>
    public class HideBannerOption : GenericOptionBase
    {
        public const string Key = "-hide_banner";

        public HideBannerOption() : base(Key) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, null);
        }
    }
}