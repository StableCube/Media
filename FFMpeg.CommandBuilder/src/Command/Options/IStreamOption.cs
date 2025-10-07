
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public interface IStreamOption : IOption
    {
        StreamSpecifier Stream { get; set; }
    }
}