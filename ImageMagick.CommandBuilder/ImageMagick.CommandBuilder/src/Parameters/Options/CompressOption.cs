
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class CompressOption : CommandOptionBase
    {
        public enum Compression
        {
            None, BZip, Fax, Group4, JPEG, JPEG2000, Lossless, LZW, RLE, Zip
        }

        public Compression CompressionType { get; set; }

        public override string Key { get { return "compress"; } }

        public override string Value { get { return CompressionType.ToString(); } }
    }
}