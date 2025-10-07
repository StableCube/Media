
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public struct CommandParam
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public CommandParam(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}