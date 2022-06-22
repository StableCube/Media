using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public class SeekOption : MainOptionBase
    {
        public const string Key = "-ss";

        /// <summary>
        /// When used as an input option (before -i), seeks in this input file to position. 
        /// Note that in most formats it is not possible to seek exactly, so ffmpeg will seek to the closest seek point before position. 
        /// When transcoding and -accurate_seek is enabled (the default), this extra segment between the seek point 
        /// and position will be decoded and discarded. When doing stream copy or when -noaccurate_seek is used, it will be preserved. 
        /// </summary>
        public string Position { get; set; }

        public SeekOption() : base (Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(OptionKey, Position);
        }
    }
}