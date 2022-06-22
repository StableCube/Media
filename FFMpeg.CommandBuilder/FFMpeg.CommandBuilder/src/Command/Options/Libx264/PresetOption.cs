using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public enum Preset
    {
        UltraFast, SuperFast, VeryFast, Faster, 
        Fast, Medium, Slow, Slower, VerySlow,
    }

    public class PresetOption : Libx264OptionBase
    {
        public const string Key = "-preset";

        ///<summary>
        /// A preset is a collection of options that will provide a certain encoding speed to compression ratio. 
        /// A slower preset will provide better compression (compression is quality per filesize). This means that, for example, 
        /// if you target a certain file size or constant bit rate, you will achieve better quality with a slower preset. 
        /// Similarly, for constant quality encoding, you will simply save bitrate by choosing a slower preset. 
        ///</summary>
        public Preset PresetValue { get; set; }

        public PresetOption() : base(Key, null) {}

        public PresetOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, PresetValue.ToString().ToLower());
        }

        public static Preset PresetFromString(string presetStr)
        {
            foreach(Preset preset in Enum.GetValues(typeof(Preset)))
                if(preset.ToString() == presetStr)
                    return preset;

            throw new ArgumentOutOfRangeException("Preset");
        }

    }
}