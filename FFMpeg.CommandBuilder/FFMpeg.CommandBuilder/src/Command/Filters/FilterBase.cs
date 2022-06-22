using System;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public enum CoordinateEval
    {
        Init,
        Frame
    }

    /// <summary>
    /// A filter is represented by a string of the form: [in_link_1]...[in_link_N]filter_name@id=arguments[out_link_1]...[out_link_M] 
    /// </summary>
    public abstract class FilterBase : IFFMpegFilter
    {
        /// <summary>
        /// filter_name is the name of the filter class of which the described filter is an instance of, and has to 
        /// be the name of one of the filter classes registered in the program optionally followed by "@id". 
        /// The name of the filter class is optionally followed by a string "=arguments". 
        /// </summary>
        public string FilterName { get; set; }

        public abstract List<CommandParam> GetFilterParams();

        public override string ToString()
        {
            List<string> paramList = new List<string>();
            foreach (var parameter in GetFilterParams())
            {
                if(parameter.Key != null && parameter.Value == null)
                {
                    paramList.Add($"{parameter.Key}");
                }
                else if(parameter.Key == null && parameter.Value != null)
                {
                    paramList.Add($"{parameter.Value}");
                }
                else
                {
                    paramList.Add($"{parameter.Key}={parameter.Value}");
                }
            }

            string result = String.Empty;
            if(paramList.Count == 0)
            {
                result = $"{FilterName}";
            }
            else
            {
                result = $"{FilterName}={String.Join(":", paramList)}";
            }

            return result;
        }
    }
}