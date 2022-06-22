
namespace StableCube.Media.FFMpeg.CommandBuilder.Options.AAC
{
    public enum Profile
    {
        aac_low,//The default, AAC "Low-complexity" profile. Is the most compatible and produces decent quality. 
        mpeg2_aac_low,//Equivalent to -profile:a aac_low -aac_pns 0. PNS was introduced with the MPEG4 specifications. 
        aac_ltp,//Long term prediction profile, is enabled by and will enable the aac_ltp option. Introduced in MPEG4. 
        aac_main,//Main-type prediction profile, is enabled by and will enable the aac_pred option. Introduced in MPEG2. 
    };

    public class ProfileOption : AACOptionBase
    {
        public const string Key = "-profile";

        public Profile Value { get; set; }

        public ProfileOption() : base(Key, null) {}

        public ProfileOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, Value.ToString());
        }
    }
}