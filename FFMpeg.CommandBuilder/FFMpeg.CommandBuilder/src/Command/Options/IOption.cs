
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    [JsonInterfaceConverter(typeof(OptionJsonConverter))]
    public interface IOption
    {
        string OptionKey { get; }

        CommandParam GetCommandParameter();
    }
}