using Ardalis.SmartEnum;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MasrafProject.Infrastructure.Convertes;

public sealed class SmartEnumJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (typeToConvert is null) return false;
        var t = typeToConvert;
        while (t != null)
        {
            if (t.IsGenericType)
            {
                var def = t.GetGenericTypeDefinition();
                if (def == typeof(SmartEnum<,>) || def == typeof(SmartEnum<>))
                    return true;
            }
            t = t.BaseType;
        }
        return false;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(SmartEnumJsonConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter?)Activator.CreateInstance(converterType)!;
    }

    private sealed class SmartEnumJsonConverter<T> : JsonConverter<T> where T : class
    {
        private readonly MethodInfo? _fromName;
        private readonly MethodInfo? _fromValue;
        private readonly PropertyInfo? _nameProperty;

        public SmartEnumJsonConverter()
        {
            var type = typeof(T);
            _fromName = type.GetMethod("FromName", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string), typeof(bool) }, null)
                ?? type.GetMethod("FromName", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null);

            _fromValue = type.GetMethod("FromValue", BindingFlags.Public | BindingFlags.Static);

            _nameProperty = type.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
        }

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;

            if (reader.TokenType == JsonTokenType.String && _fromName != null)
            {
                var name = reader.GetString()!;
                try
                {
                    var parameters = _fromName.GetParameters().Length == 2 ? new object[] { name, false } : new object[] { name };
                    var result = _fromName.Invoke(null, parameters);
                    return result as T;
                }
                catch
                {
                    return null;
                }
            }

            if ((reader.TokenType == JsonTokenType.Number || reader.TokenType == JsonTokenType.String) && _fromValue != null)
            {

                if (reader.TryGetInt32(out var intVal))
                {
                    try
                    {
                        var result = _fromValue.Invoke(null, new object[] { intVal });
                        return result as T;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    if (int.TryParse(s, out var v))
                    {
                        try
                        {
                            var result = _fromValue.Invoke(null, new object[] { v });
                            return result as T;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }

            try
            {
                var json = JsonSerializer.Deserialize(ref reader, typeToConvert, options);
                return json as T;
            }
            catch
            {
                return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            if (_nameProperty != null)
            {
                var name = _nameProperty.GetValue(value) as string;
                writer.WriteStringValue(name);
                return;
            }
            writer.WriteStringValue(value.ToString());
        }
    }
}