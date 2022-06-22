using System;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public enum Profile
    {
        Baseline, Main, High, High10, High422, High444,
    }

    public class ProfileOption : Libx264OptionBase
    {
        public const string Key = "-profile";

        ///<summary>
        /// Another optional setting is -profile:v which will limit the output to a specific H.264 profile. 
        /// Omit this unless your target device only supports a certain profile (see Compatibility). 
        /// Current profiles include: baseline, main, high, high10, high422, high444. 
        /// Note that usage of -profile:v is incompatible with lossless encoding. 
        ///</summary>
        public Profile ProfileValue { get; set; }

        public ProfileOption() : base(Key, null) {}

        public ProfileOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            return new CommandParam(StreamOptionKey, ProfileValue.ToString().ToLower());
        }

        public static Profile ProfileFromString(string profileStr)
        {
            foreach(Profile profile in Enum.GetValues(typeof(Profile)))
                if(profile.ToString() == profileStr)
                    return profile;

            throw new ArgumentOutOfRangeException("Profile");
        }

    }
}