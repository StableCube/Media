using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StableCube.Media.ImageMagick.CommandBuilder
{
    public class CommandOptionJsonConverter : JsonConverter<ICommandOption>
    {
        private const string TypePropertyName = "key";

        private Dictionary<string, Type> TypeMap = new Dictionary<string, Type>()
        {
            { "resize", typeof(ResizeOption) },
            { "size", typeof(SizeOption) },
            { "background", typeof(BackgroundOption) },
            { "compress", typeof(CompressOption) },
            { "geometry", typeof(GeometryOption) },
            { "compose", typeof(ComposeOption) },
            { "composite", typeof(CompositeOption) },
            { "gravity", typeof(GravityOption) },
            { "swap", typeof(SwapOption) },
            { "layers", typeof(LayersOption) },
            { "coalesce", typeof(CoalesceOption) },
            { "border", typeof(BorderOption) },
            { "bordercolor", typeof(BorderColorOption) },
            { "define", typeof(DefineOption) },
            { "quality", typeof(QualityOption) },
            { "extent", typeof(ExtentOption) },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(ICommandOption));
        }

        public override ICommandOption Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                        return JsonSerializer.Deserialize(ref readerAtStart, toolType, options) as ICommandOption;
                    }
                }

                throw new NotSupportedException($"{type ?? "<unknown>"} can not be deserialized");
            }
        }

        public override void Write(Utf8JsonWriter writer, ICommandOption value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value as object, value.GetType(), options);
        }
    }
}
