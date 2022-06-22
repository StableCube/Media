
namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class WatermarkParameters
    {
        public enum AlignmentPositions
        {
            N, NE, E, SE, S, SW, W, NW, C
        }

        public AlignmentPositions Position { get; set; }

        public int InDelay { get; set; }

        public int OutDelay { get; set; }

        public int FadeTimeMilliseconds { get; set; }
    }
}
