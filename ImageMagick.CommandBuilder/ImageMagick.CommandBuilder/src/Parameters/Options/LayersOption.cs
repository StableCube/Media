using System;

namespace StableCube.Media.ImageMagick.CommandBuilder
{

        public enum  LayerMethod
        {
            CompareAny,
            CompareClear,
            CompareOverlay,
            Coalesce,
            Composite,
            Dispose,
            Flatten,
            Merge,
            Mosaic,
            Optimize,
            OptimizeFrame,
            OptimizePlus,
            OptimizeTransparency,
            RemoveDups,
            RemoveZero,
            TrimBounds
        }
        
    public class LayersOption : CommandOptionBase
    {
        public LayerMethod Method { get; set; }

        public override string Key { get { return "layers"; } }

        public override string Value { get { return GetValueString(); } }

        private string GetValueString()
        {
            switch (Method)
            {
                case LayerMethod.CompareAny:
                    return "compare-any";
                case LayerMethod.CompareClear:
                    return "compare-clear";
                case LayerMethod.CompareOverlay:
                    return "compare-overlay";
                case LayerMethod.Coalesce:
                    return "coalesce";
                case LayerMethod.Composite:
                    return "composite";
                case LayerMethod.Dispose:
                    return "dispose";
                case LayerMethod.Flatten:
                    return "flatten";
                case LayerMethod.Merge:
                    return "merge";
                case LayerMethod.Mosaic:
                    return "mosaic";
                case LayerMethod.Optimize:
                    return "optimize";
                case LayerMethod.OptimizeFrame:
                    return "optimize-frame";
                case LayerMethod.OptimizePlus:
                    return "optimize-plus";
                case LayerMethod.OptimizeTransparency:
                    return "optimize-transparency";
                case LayerMethod.RemoveDups:
                    return "remove-dups";
                case LayerMethod.RemoveZero:
                    return "remove-zero";
                case LayerMethod.TrimBounds:
                    return "trim-bounds";
                default:
                    throw new ArgumentOutOfRangeException("Method: " + Method.ToString());
            }
        }
    }
}