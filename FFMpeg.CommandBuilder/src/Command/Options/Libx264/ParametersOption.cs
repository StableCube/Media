using System;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx264
{
    public class ParametersOption : Libx264OptionBase
    {
        public const string Key = "-x264-params";

        ///<summary> 
        /// Set any x264 option, see x264 --fullhelp for a list.
        /// Argument is a list of key=value couples separated by ":". In filter and psy-rd options that 
        /// use ":" as a separator themselves, use "," instead. They accept it as well since long ago but 
        /// this is kept undocumented for some reason.
        ///<summary>
        public List<SerializableParameter> Parameters { get; set; } = new List<SerializableParameter>();

        public ParametersOption() : base(Key, null) {}

        public ParametersOption(StreamSpecifier stream) : base(Key, stream) {}

        public override CommandParam GetCommandParameter()
        {
            List<string> values = new List<string>();
            foreach (var param in Parameters)
            {
                if(param.Key != null)
                    values.Add(param.Key + "=" + param.Value);
            }

            string value = String.Join(":", values);

            return new CommandParam(StreamOptionKey, value);
        }
    }
}