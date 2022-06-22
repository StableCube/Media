
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx265
{
    public enum Preset
    {
        UltraFast,
        SuperFast,
        VeryFast,
        Faster,
        Fast,
        Medium,
        Slow,
        Slower,
        VerySlow,
        Placebo,
    }

    public class PresetOption : Libx265OptionBase
    {
        public const string Key = "-preset";

        ///<summary>
        /// Choose a preset. The default is medium. The preset determines how fast the encoding process will be – at the expense 
        /// of compression efficiency. Put differently, if you choose ultrafast, the encoding process is going to run fast, 
        /// but the file size will be larger when compared to medium. The visual quality will be the same. Valid presets are 
        /// ultrafast, superfast, veryfast, faster, fast, medium, slow, slower, veryslow and placebo.
        ///</summary>
        public Preset Value { get; set; }

        public PresetOption() : base(Key, null) {}

        public PresetOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Value.ToString().ToLower());
        }
    }
}