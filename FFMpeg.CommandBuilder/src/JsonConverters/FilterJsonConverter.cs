using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class FilterJsonConverter : JsonConverter<IFFMpegFilter>
    {
        private const string TypePropertyName = "filterName";

        private Dictionary<string, Type> TypeMap = new Dictionary<string, Type>()
        {
            { "aselect", typeof(ASelectFilter) },
            { "select", typeof(SelectFilter) },
            { "fade", typeof(FadeFilter) },
            { "format", typeof(FormatFilter) },
            { "fps", typeof(FPSFilter) },
            { "loop", typeof(LoopFilter) },
            { "overlay", typeof(OverlayFilter) },
            { "pad", typeof(PadFilter) },
            { "scale", typeof(ScaleFilter) },
            { "setpts", typeof(SetPTSFilter) },
            { "trim", typeof(TrimFilter) },
            { "thumbnail", typeof(ThumbnailFilter) },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(IFFMpegFilter));
        }

        public override IFFMpegFilter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) 
                return null;
            
            var readerAtStart = reader;

            using(var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                var jsonObject = jsonDocument.RootElement;
                JsonElement typeEl;
                if(!jsonObject.TryGetProperty(TypePropertyName, out typeEl))
                    throw new JsonException($"Property '{TypePropertyName}' not found");

                string type = typeEl.GetString();

                if (!string.IsNullOrEmpty(type))
                {
                    Type toolType;
                    if(TypeMap.TryGetValue(type, out toolType))
                    {
                        return JsonSerializer.Deserialize(ref readerAtStart, toolType, options) as IFFMpegFilter;
                    }
                }

                throw new NotSupportedException($"{type ?? "<unknown>"} can not be deserialized");
            }
        }

        public override void Write(Utf8JsonWriter writer, IFFMpegFilter value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value as object, value.GetType(), options);
        }
    }
}
