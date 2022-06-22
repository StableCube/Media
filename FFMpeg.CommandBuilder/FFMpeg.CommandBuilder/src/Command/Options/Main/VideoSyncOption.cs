using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Main
{
    public enum VideoSyncMethod
    {
        Passthrough,
        CFR,
        VFR,
        Auto,
        Drop
    }

    public class VideoSyncOption : MainOptionBase
    {
        public const string Key = "-vsync";

        /// <summary>
        /// Video sync method. For compatibility reasons old values can be specified as numbers. 
        /// Newly added values will have to be specified as strings always. 
        /// </summary>
        public VideoSyncMethod Method { get; set; }

        public VideoSyncOption() : base(Key, null) {}

        public VideoSyncOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            string strVal = "";
            switch(Method)
            {
                case VideoSyncMethod.Auto:
                    strVal = "-1";
                break;
                case VideoSyncMethod.CFR:
                    strVal = "1";
                break;
                case VideoSyncMethod.VFR:
                    strVal = "2";
                break;
                case VideoSyncMethod.Passthrough:
                    strVal = "0";
                break;
                case VideoSyncMethod.Drop:
                    strVal = "drop";
                break;
                default:
                    throw new ArgumentOutOfRangeException("Method");
            }

            return new CommandParam(StreamOptionKey, strVal);
        }
    }
}