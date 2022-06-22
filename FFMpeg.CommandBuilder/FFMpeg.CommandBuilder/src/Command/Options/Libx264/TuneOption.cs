using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public enum Tune
    {
        Film,//use for high quality movie content; lowers deblocking 
        Animation,//good for cartoons; uses higher deblocking and more reference frames 
        Grain,//preserves the grain structure in old, grainy film material 
        StillImage,//good for slideshow-like content 
        FastDecode,//allows faster decoding by disabling certain filters 
        ZeroLatency,//good for fast encoding and low-latency streaming 
    }

    public class TuneOption : Libx264OptionBase
    {
        public const string Key = "-tune";

        ///<summary>
        /// You can optionally use -tune to change settings based upon the specifics of your input. Current tunings include:
        ///     film – use for high quality movie content; lowers deblocking
        ///     animation – good for cartoons; uses higher deblocking and more reference frames
        ///     grain – preserves the grain structure in old, grainy film material
        ///     stillimage – good for slideshow-like content
        ///     fastdecode – allows faster decoding by disabling certain filters
        ///     zerolatency – good for fast encoding and low-latency streaming
        ///</summary>
        public Tune TuneValue { get; set; }

        public TuneOption() : base(Key, null) {}

        public TuneOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, TuneValue.ToString().ToLower());
        }

        public static Tune TuneFromString(string tuneStr)
        {
            foreach(Tune tune in Enum.GetValues(typeof(Tune)))
                if(tune.ToString() == tuneStr)
                    return tune;

            throw new ArgumentOutOfRangeException("Tune");
        }
    }
}