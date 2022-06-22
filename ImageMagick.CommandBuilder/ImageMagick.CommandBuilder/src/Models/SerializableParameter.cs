
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    /// <summary>
    /// Serializable parameter
    /// </summary>
    public class SerializableParameter
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public SerializableParameter()
        {
        }

        public SerializableParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}