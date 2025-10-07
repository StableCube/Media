using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace StableCube.Media.FFMpeg.CommandBuilder
{
    public class OptionJsonConverter : JsonConverter<IOption>
    {
        private static IEnumerable<Type> _assemblyCache = new Type[0];
        private static bool _dupeCheckRan = false;
        private static Dictionary<string, HashSet<string>> _dupeCheck = new Dictionary<string, HashSet<string>>();

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(IOption));
        }

        public override IOption Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) 
                return null;
            
            var readerAtStart = reader;

            using(var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                var jsonObject = jsonDocument.RootElement;
                var propOptionType = jsonObject.GetProperty("optionType").GetString();
                var propOptionKey = jsonObject.GetProperty("optionKey").GetString();
                Type optionType = GetOptionType(propOptionType, propOptionKey);

                return JsonSerializer.Deserialize(ref readerAtStart, optionType, options) as IOption;
            }
        }

        public override void Write(Utf8JsonWriter writer, IOption value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value as object, value.GetType(), options);
        }

        /// <summary>
        /// Use reflection to find the concrete type of an option so we do not have to do dozens of options by hand...
        /// </summary>
        /// <param name="optionTypeId"></param>
        /// <param name="optionKey"></param>
        /// <returns></returns>
        private Type GetOptionType(string optionTypeId, string optionKey)
        {
            if(_assemblyCache.Count() == 0)
            {
                _assemblyCache = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IOption).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);
            }

            EnsureNoDupe(optionTypeId, optionKey);

            foreach (Type optionType in _assemblyCache)
            {
                var keyField = optionType.GetField("Key", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                if(keyField == null)
                    throw new NullReferenceException(optionType.FullName + " must implement a \"Key\" const");

                var optionTypeField = optionType.GetField("OptionTypeId", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                if(optionTypeField == null)
                    throw new NullReferenceException(optionType.FullName + " must implement a \"OptionTypeId\" const");

                string optionTypeVal = (string)optionTypeField.GetRawConstantValue();
                string keyFieldVal = (string)keyField.GetRawConstantValue();

                if(optionTypeId == optionTypeVal && keyFieldVal == optionKey)
                {
                    try
                    {
                        return optionType;
                    }
                    catch (System.MissingMethodException e)
                    {
                        throw new MissingMethodException(optionType.FullName + " must have a parameterless constructor. " + e.Message);
                    }
                }
            }

            throw new ArgumentOutOfRangeException($"Could not find concrete class to deserialize the option \"{optionKey}\"");
        }

        private void EnsureNoDupe(string optionTypeId, string optionKey)
        {
            if(!_dupeCheckRan)
            {
                foreach (Type optionType in _assemblyCache)
                {
                    var keyField = optionType.GetField("Key", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    if(keyField == null)
                        throw new NullReferenceException(optionType.FullName + " must implement a \"Key\" const");

                    var optionTypeField = optionType.GetField("OptionTypeId", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    if(optionTypeField == null)
                        throw new NullReferenceException(optionType.FullName + " must implement a \"OptionTypeId\" const");

                    string optionTypeVal = (string)optionTypeField.GetRawConstantValue();
                    string keyFieldVal = (string)keyField.GetRawConstantValue();

                    if(!_dupeCheck.ContainsKey(optionTypeVal))
                        _dupeCheck.Add(optionTypeVal, new HashSet<string>());

                    if(_dupeCheck[optionTypeVal].Contains(keyFieldVal))
                        throw new InvalidCastException($"{optionType.FullName} is using duplicate type: {optionTypeVal}, key: {keyFieldVal}");

                    _dupeCheck[optionTypeVal].Add(keyFieldVal);
                }

                _dupeCheckRan = true;
            }
        }
    }
}
