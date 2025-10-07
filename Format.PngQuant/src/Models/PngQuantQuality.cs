using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.PngQuant
{
    public struct PngQuantQuality
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public PngQuantQuality(int min, int max)
        {
            Min = Clamp(min, 0, 100);
            Max = Clamp(max, 0, 100);

            if(Min > Max)
                Min = Max;
        }

        private static int Clamp(int value, int min, int max)
        {
            if(value < min)
                value = min;

            if(value > max)
                value = max;

            return value;
        }
    }
}
