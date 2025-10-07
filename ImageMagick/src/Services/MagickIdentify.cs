
namespace StableCube.Media.ImageMagick
{
    public class MagickIdentify : MagickToolBase, IMagickIdentify
    {
        public static string ToolName { get { return "identify"; } }

        public MagickIdentify() : base(ToolName)
        {
        }

        public MagickIdentify(string magickBinaryPath) : base(magickBinaryPath, ToolName)
        {
        }
    }
}