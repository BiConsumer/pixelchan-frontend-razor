﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pixelchan.Converters {

    public class UnixDateTimeConverter : DateTimeConverterBase {

        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteRawValue(((DateTime) value - EPOCH).TotalMilliseconds + "000");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.Value == null) { return null; }

            return DateTimeOffset.FromUnixTimeMilliseconds((long) reader.Value).UtcDateTime.ToLocalTime();
        }
    }
}