using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StableCube.Media.FFProbe.DataModel
{
    /// <summary>
    /// Converts booleans stored as strings to boolean value type
    /// </summary>
    public class StringBooleanConverter : JsonConverter<bool>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(bool));
        }

        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            string chkValue = value.ToLower();
            if (chkValue.Equals("true") ||chkValue.Equals("yes") || chkValue.Equals("1") )
            {
                return true;
            }
            if (value.ToLower().Equals("false") ||chkValue.Equals("no") || chkValue.Equals("0"))
            {
                return false;
            }
            throw new JsonException();

        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value as object, value.GetType(), options);
        }
    }
}
