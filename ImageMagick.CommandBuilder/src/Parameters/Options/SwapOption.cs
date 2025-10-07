
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class SwapOption : CommandOptionBase
    {
        public int Index1 { get; set; }

        public int Index2 { get; set; }

        public override string Key { get { return "swap"; } }

        public override string Value { get { return Index1.ToString() + "," + Index2.ToString(); } }
    }
}