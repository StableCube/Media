using System;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder.Options.Libx265
{
    public class ParametersOption : Libx265OptionBase
    {
        public const string Key = "-x264-params";

        /// <summary>
        /// Generally, options are passed to x265 with the -x265-params argument. 
        /// For fine-tuning the encoding process, you can therefore pass any option that is listed in the ​x265 documentation. 
        /// Keep in mind that fine-tuning any of the options is generally not necessary, unless you absolutely know what you 
        /// need to change. 
        /// </summary>
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