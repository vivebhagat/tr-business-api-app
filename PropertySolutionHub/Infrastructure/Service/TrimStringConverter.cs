﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace PropertySolutionHub.Infrastructure.Service
{
    public class TrimStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString()?.Trim();
            }
            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.Trim());
        }
    }
}
