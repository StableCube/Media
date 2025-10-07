
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    [JsonInterfaceConverter(typeof(FilterJsonConverter))]
    public interface IFFMpegFilter : IFFMpegFilterParameters
    {
        string FilterName { get; }
    }
}