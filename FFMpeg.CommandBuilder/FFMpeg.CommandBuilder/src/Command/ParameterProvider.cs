using System;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public abstract class ParameterProvider : IParameterProvider
    {
        public abstract List<CommandParam> GetParameters();

        /// <summary>
        /// Terminal command string
        /// </summary>
        public override string ToString()
        {
            List<CommandParam> parameters = GetParameters();
            List<string> values = new List<string>();
            foreach (var param in parameters)
            {
                if(param.Key != null)
                    values.Add(param.Key);

                if(param.Value != null)
                    values.Add(param.Value);
            }

            return String.Join(" ", values);
        }
    }
}