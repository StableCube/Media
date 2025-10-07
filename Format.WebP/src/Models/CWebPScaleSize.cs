namespace StableCube.Media.WebP
{
    public struct CWebPResizeDimensions
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public CWebPResizeDimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
