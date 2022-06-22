using System;
using System.Text.Json.Serialization;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    /// <summary>
    /// Work around to use json converter on interface
    /// 
    /// This should be able to be removed in dotnet 6
    /// 
    /// https://github.com/dotnet/runtime/issues/33112
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class JsonInterfaceConverter : JsonConverterAttribute
    {
        public JsonInterfaceConverter(Type converterType) : base(converterType)
        {
        }
    }
}