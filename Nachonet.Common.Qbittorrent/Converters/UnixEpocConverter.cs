using System;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Converters
{
    public class UnixEpocConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader,
                                        Type typeToConvert,
                                        JsonSerializerOptions options)
        {
            // if (reader.TokenType == JsonTokenType.Null)
            //    System.Console.WriteLine("abc");
            var epochSeconds = reader.GetInt64();
            if (epochSeconds < 0)
            {
                return DateTime.MinValue;
            }

            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochSeconds);
            return dateTimeOffset.LocalDateTime;
        }

        public override void Write(Utf8JsonWriter writer,
                                    DateTime value,
                                    JsonSerializerOptions options)
        {
            if (value < new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            {
                writer.WriteNumberValue(-39600);
                return;
            }

            var epochSeconds = new DateTimeOffset(value).ToUnixTimeSeconds();
            writer.WriteNumberValue(epochSeconds);
        }
    }
}