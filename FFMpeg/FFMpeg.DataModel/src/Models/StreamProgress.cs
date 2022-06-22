
namespace StableCube.Media.FFMpeg.DataModel
{
    public struct StreamProgress
    {
        public int Number  { get; set; }

        public int Index  { get; set; }

        public float Value  { get; set; }

        public StreamProgress(int number, int index, float value)
        {
            Number = number;
            Index = index;
            Value = value;
        }
    }
}