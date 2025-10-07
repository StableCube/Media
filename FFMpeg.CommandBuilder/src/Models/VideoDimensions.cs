
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public struct VideoDimensions
    {
        public int Width { get; private set; }

        public int Height { get; private set; }

        public VideoDimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
