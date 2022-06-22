
namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public enum ComposeType
    {
        Multiply, Screen, Plus, Add, Minus, Subtract, Difference, Exclusion, Darken, Lighten,
        LinearDodge, LinearBurn, ColorDodge, ColorBurn, Overlay, HardLight, LinearLight, SoftLight,
        PegtopLight, VividLight, PinLight
    }

    public class CompositionMethod
    {
        public ComposeType Type { get; set; }

        public CompositionMethod()
        {
        }

        public CompositionMethod(ComposeType type)
        {
            Type = type;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case ComposeType.LinearDodge:
                    return "linear-dodge";
                case ComposeType.LinearBurn:
                    return "linear-burn";
                case ComposeType.ColorDodge:
                    return "color-dodge";
                case ComposeType.ColorBurn:
                    return "color-burn";
                case ComposeType.HardLight:
                    return "hard-light";
                case ComposeType.LinearLight:
                    return "linear-light";
                case ComposeType.SoftLight:
                    return "soft-light";
                case ComposeType.PegtopLight:
                    return "pegtop-light";
                case ComposeType.VividLight:
                    return "vivid-light";
                case ComposeType.PinLight:
                    return "pin-light";
            }

            return Type.ToString().ToLower();
        }
    }
}