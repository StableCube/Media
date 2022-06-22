
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public struct ImageDimensions
    {
        public int Width { get; private set; }
        
        public int Height { get; private set; }

        public ImageDimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}