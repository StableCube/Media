using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class BitrateOption : MainOptionBase
    {
        public const string Key = "-b";

        /// <summary>Set bitrate in kilobits</summary>
        public int Bitrate { get; set; }

        public BitrateOption() : base(Key, null) {}

        public BitrateOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            var value = MathHelper.Clamp(Bitrate, 0, int.MaxValue);
            
            return new CommandParam(StreamOptionKey, value.ToString());
        }
    }
}