using System;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public enum EndOfFileAction
    {
        Repeat,
        EndAll,
        Pass
    }

    /// <summary>
    /// Some filters with several inputs support a common set of options. These options can only be set by name, not with the short notation. 
    /// </summary>
    public class MultiInputFilterOptions : IFFMpegFilterParameters
    {
        /// <summary>
        /// The action to take when EOF is encountered on the secondary input; it accepts one of the following values:
        /// repeat: Repeat the last frame (the default). 
        /// endall: End both streams. 
        /// pass: Pass the main input through. 
        /// </summary>
        /// <value></value>
        public EndOfFileAction? EndOfFileAction { get; set; }

        /// <summary>
        /// If set to 1, force the output to terminate when the shortest input terminates. Default value is 0. 
        /// </summary>
        /// <value></value>
        public bool? Shortest { get; set; }

        /// <summary>
        /// If set to 1, force the filter to extend the last frame of secondary streams until the end of the primary stream. 
        /// A value of 0 disables this behavior. Default value is 1. 
        /// </summary>
        /// <value></value>
        public bool? RepeatLast { get; set; }

        public List<CommandParam> GetFilterParams()
        {
            List<CommandParam> paramList = new List<CommandParam>();

            if(Shortest.HasValue)
                paramList.Add(new CommandParam("shortest", Shortest.Value == true ? "1" : "0"));

            if(EndOfFileAction.HasValue)
                paramList.Add(new CommandParam("eof_action", EndOfFileAction.Value.ToString().ToLower()));

            if(RepeatLast.HasValue)
                paramList.Add(new CommandParam("repeatlast", RepeatLast.Value == true ? "1" : "0"));

            return paramList;
        }

        public string GetFilterString()
        {
            List<string> paramList = new List<string>();

            foreach (var parameter in GetFilterParams())
            {
                paramList.Add(parameter.Key + "=" + parameter.Value);
            }

            return String.Join(":", paramList);
        }
    }
}
