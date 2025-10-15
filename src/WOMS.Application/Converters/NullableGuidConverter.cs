using System.Text.Json;
using System.Text.Json.Serialization;

namespace WOMS.Application.Converters
{
    public class NullableGuidConverter : JsonConverter<Guid?>
    {
        public override Guid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                
                // Handle common invalid values that might be sent from frontend
                if (string.IsNullOrEmpty(stringValue) || 
                    stringValue.Equals("N", StringComparison.OrdinalIgnoreCase) ||
                    stringValue.Equals("null", StringComparison.OrdinalIgnoreCase) ||
                    stringValue.Equals("undefined", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                // Try to parse as GUID
                if (Guid.TryParse(stringValue, out var guid))
                {
                    return guid;
                }
            }

            throw new JsonException($"Unable to convert \"{reader.GetString()}\" to Guid?.");
        }

        public override void Write(Utf8JsonWriter writer, Guid? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
