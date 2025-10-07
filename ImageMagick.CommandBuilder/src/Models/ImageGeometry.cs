
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class ImageGeometry
    {
        /// <summary>
        /// Gets or sets a value indicating whether the image is resized based on the smallest fitting dimension (^).
        /// </summary>
        public bool FillArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image is resized if image is greater than size (&gt;)
        /// </summary>
        public bool Greater { get; set; }

        /// <summary>
        /// Gets or sets the height of the geometry.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image is resized without preserving aspect ratio (!)
        /// </summary>
        public bool IgnoreAspectRatio { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the width and height are expressed as percentages.
        /// </summary>
        public bool IsPercentage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image is resized if the image is less than size (&lt;)
        /// </summary>
        public bool Less { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the image is resized using a pixel area count limit (@).
        /// </summary>
        public bool LimitPixels { get; set; }

        /// <summary>
        /// Gets or sets the width of the geometry.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the X offset from origin.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y offset from origin.
        /// </summary>
        public int Y { get; set; }

        internal bool AspectRatio
        {
            get;
            set;
        }

        public override string ToString()
        {
            string result = null;

            if (AspectRatio)
                return Width + ":" + Height;

            if (Width > 0)
                result += Width;

            if (Height > 0)
                result += "x" + Height;

            if (X != 0 || Y != 0)
            {
                if (X >= 0)
                    result += '+';

                result += X;

                if (Y >= 0)
                    result += '+';

                result += Y;
            }

            if (IsPercentage)
                result += '%';

            if (IgnoreAspectRatio)
                result += '!';

            if (Greater)
                result += '>';

            if (Less)
                result += '<';

            if (FillArea)
                result += '^';

            if (LimitPixels)
                result += '@';

            return result;
        }
    }
}