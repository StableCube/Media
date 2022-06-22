namespace StableCube.Media.WebP
{
    public struct Gif2WebPKMinMax
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public Gif2WebPKMinMax(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}
