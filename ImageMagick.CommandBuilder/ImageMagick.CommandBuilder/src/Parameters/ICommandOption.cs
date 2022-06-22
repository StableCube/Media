
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    [JsonInterfaceConverter(typeof(CommandOptionJsonConverter))]
    public interface ICommandOption
    {
        bool Alternate { get; set; }
        
        string Key { get; }

        string Value { get; }

        string ToString();

        ImageMagickParameter GetParameter();
    }
}