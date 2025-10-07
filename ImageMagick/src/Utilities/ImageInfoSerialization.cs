using System;
using System.Text.Json;
using StableCube.Media.ImageMagick.DataModel;

namespace StableCube.Media.ImageMagick
{
    /// <summary>
    /// Convert the broken json output from ImageMagick into something less fucking stupid
    /// </summary>
    public class ImageInfoSerialization
    {
        private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static ImageInfo FromJson(string jsonArray, string filePath)
        {
            if(String.IsNullOrEmpty(jsonArray))
                throw new ArgumentNullException("jsonArraychunkLength");

            jsonArray = jsonArray.Trim();
            
            if(!jsonArray.StartsWith("[") || !jsonArray.EndsWith("]"))
                throw new InfoProbeException($"Expecting a json array but got: {jsonArray}");

            string json = $"{{ \"frames\": {jsonArray} }}";
            
            ImageMagickInfoRoot result = JsonSerializer.Deserialize<ImageMagickInfoRoot>(json, _serializerOptions);

            ImageInfo info = new ImageInfo()
            {
                Path = filePath,
                Format = result.Frames[0].Image.Format,
                FormatDescription = result.Frames[0].Image.FormatDescription,
                MimeType = result.Frames[0].Image.MimeType,
                Class = result.Frames[0].Image.Class,
                Properties = result.Frames[0].Image.Properties,
                Version = result.Frames[0].Image.Version,
                FrameCount = result.Frames[0].Image.Scenes,
                Iterations = result.Frames[0].Image.Iterations,
                Delay = result.Frames[0].Image.Delay,
                Endianess = result.Frames[0].Image.Endianess,
                Interlace = result.Frames[0].Image.Interlace,
                Compression = result.Frames[0].Image.Compression,
                Geometry = result.Frames[0].Image.Geometry,
            };

            info.Frames = new ImageFrame[result.Frames.Length];
            for (int i = 0; i < result.Frames.Length; i++)
            {
                var source = result.Frames[i].Image;

                info.Frames[i] = new ImageFrame()
                {
                    FrameIndex = source.Scene,
                    PageGeometry = source.PageGeometry,
                    Units = source.Units,
                    Type = source.Type,
                    Colorspace = source.Colorspace,
                    Depth = source.Depth,
                    BaseDepth = source.BaseDepth,
                    ChannelDepth = source.ChannelDepth,
                    Pixels = source.Pixels,
                    ColorMap = source.ColorMap,
                    ImageStatistics = source.ImageStatistics,
                    ChannelStatistics = source.ChannelStatistics,
                    RenderingIntent = source.RenderingIntent,
                    Gamma = source.Gamma,
                    MatteColor = source.MatteColor,
                    BackgroundColor = source.BackgroundColor,
                    BorderColor = source.BorderColor,
                    TransparentColor = source.TransparentColor,
                    Intensity = source.Intensity,
                    Compose = source.Compose,
                    Dispose = source.Dispose,
                    Orientation = source.Orientation,
                    Tainted = source.Tainted,
                    Filesize = source.Filesize,
                    NumberPixels = source.NumberPixels,
                    PixelsPerSecond = source.PixelsPerSecond,
                    UserTime = source.UserTime,
                    ElapsedTime = source.ElapsedTime,
                    Chromaticity = source.Chromaticity,
                    Quality = source.Quality,
                };
            }

            return info;
        }
    }

    public class ImageMagickInfoRoot
    {
        public ImageMagickInfoInner[] Frames { get; set; }
    }

    public class ImageMagickInfoInner
    {
        public ImageMagickImage Image { get; set; }
    }
}