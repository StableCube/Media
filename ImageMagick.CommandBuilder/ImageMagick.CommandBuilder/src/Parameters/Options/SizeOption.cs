
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class SizeOption : CommandOptionBase
    {
        public ImageGeometry Geometry { get; set; } = new ImageGeometry();

        public ImageColor Canvas { get; set; }

        public override string Key { get { return "size"; } }

        public override string Value 
        { 
            get 
            {
                string result = Geometry.ToString();
                if(Canvas != null)
                    result += " xc:" + Canvas.ColorString;

                return result;
            } 
        }
    }
}