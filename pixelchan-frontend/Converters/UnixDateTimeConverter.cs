namespace Pixelchan.Converters;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class UnixDateTimeConverter : DateTimeConverterBase {

    private static readonly DateTime EPOCH = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
        if (value == null) {
            return;
        }
        
        writer.WriteRawValue(((DateTime) value - EPOCH).TotalMilliseconds + "000");
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
        if (reader.Value == null) { return null; }

        return DateTimeOffset.FromUnixTimeMilliseconds((long) reader.Value).UtcDateTime.ToLocalTime();
    }
}