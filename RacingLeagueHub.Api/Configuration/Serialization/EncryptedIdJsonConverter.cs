using Newtonsoft.Json;
using RacingLeagueHub.Application.Models;

namespace RacingLeagueHub.Api.Configuration.Serialization;

public class EncryptedIdJsonConverter : JsonConverter<EncryptedId>
{
    public override void WriteJson(JsonWriter writer, EncryptedId? value, JsonSerializer serializer)
    {
        if (value is null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteValue(value.EncryptedReadonlyValue);
    }

    public override EncryptedId? ReadJson(JsonReader reader, Type objectType, EncryptedId? existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        if (reader.TokenType != JsonToken.String)
            throw new JsonSerializationException(
                $"Expected string token for EncryptedId, got {reader.TokenType}.");

        var encryptedValue = reader.Value as string
            ?? throw new JsonSerializationException("EncryptedId value was null.");

        // This will throw ArgumentException if tampered — bubbles up as 400
        return new EncryptedId(encryptedValue);
    }
}
