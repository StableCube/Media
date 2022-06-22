namespace StableCube.Media.FFMpeg.CommandFactory
{
    public struct MediaDimensions
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public MediaDimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}