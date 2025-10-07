
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Muxer
{
    public class FramePTSOption : MuxerOptionBase
    {
        public const string Key = "-frame_pts";

        /// <summary>
        /// If set to 1, expand the filename with pts from pkt->pts. Default value is 0. 
        /// </summary>
        public bool FramePts { get; set; }

        public FramePTSOption() : base(Key)
        {
        }

        public override CommandParam GetCommandParameter()
        {
            string strVal = "0";
            if(FramePts)
                strVal = "1";

            return new CommandParam(OptionKey, strVal);
        }
    }
}