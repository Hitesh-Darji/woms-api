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

    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                
                // Handle common formatting issues
                if (!string.IsNullOrEmpty(stringValue))
                {
                    // Replace semicolons with colons (common mistake)
                    stringValue = stringValue.Replace(';', ':');
                    
                    // Handle single digit hours (e.g., "9:00" -> "09:00")
                    if (stringValue.Length > 0 && char.IsDigit(stringValue[0]) && !stringValue.Contains(":"))
                    {
                        // If it's just a number, treat it as hours
                        if (int.TryParse(stringValue, out var hours))
                        {
                            return TimeSpan.FromHours(hours);
                        }
                    }
                    
                    // Try parsing with various formats
                    if (TimeSpan.TryParse(stringValue, out var timeSpan))
                        return timeSpan;
                        
                    // Try parsing with "HH:mm" format
                    if (stringValue.Length == 4 && stringValue[2] == ':')
                    {
                        var paddedValue = "0" + stringValue; // "9:00" -> "09:00"
                        if (TimeSpan.TryParse(paddedValue, out timeSpan))
                            return timeSpan;
                    }
                }
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                var ticks = reader.GetInt64();
                return TimeSpan.FromTicks(ticks);
            }

            throw new JsonException($"Unable to convert \"{reader.GetString()}\" to TimeSpan. Expected format: 'HH:mm:ss' or 'HH:mm' (e.g., '09:00' or '09:00:00').");
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(@"hh\:mm\:ss"));
        }
    }
}
