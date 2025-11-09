using System;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Converters
{
    public class UnixEpocNullableConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader,
                                        Type typeToConvert,
                                        JsonSerializerOptions options)
        {
            var epochSeconds = reader.GetInt64();
            if (epochSeconds < 0)
            {
                return null;
            }

            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochSeconds);
            return dateTimeOffset.LocalDateTime;
        }

        public override void Write(Utf8JsonWriter writer,
                                    DateTime? value,
                                    JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else if (value.Value < new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            {
                writer.WriteNumberValue(-39600);
                return;
            }
            else
            {
                var epochSeconds = new DateTimeOffset(value.Value).ToUnixTimeSeconds();
                writer.WriteNumberValue(epochSeconds);
            }
        }
    }
}