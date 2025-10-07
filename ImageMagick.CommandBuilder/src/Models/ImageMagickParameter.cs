
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public struct ImageMagickParameter
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public ImageMagickParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}