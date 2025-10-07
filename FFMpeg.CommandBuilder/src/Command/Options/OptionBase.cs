
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public abstract class OptionBase : IOption
    {
        public string OptionType { get; private set; }

        public string OptionKey { get; private set; }

        public OptionBase(string optionType, string optionKey)
        {
            OptionType = optionType;
            OptionKey = optionKey;
        }

        public abstract CommandParam GetCommandParameter();
        
    }
}