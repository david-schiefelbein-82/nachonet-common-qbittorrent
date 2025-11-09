using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nachonet.Common.Qbittorrent.Txn;

namespace Nachonet.Common.Qbittorrent.Converters
{
    public class TorrentStatusConverter : JsonConverter<TorrentStatus>
    {
        public override TorrentStatus Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                TorrentStatusExtn.Parse(reader.GetString()!);

        public override void Write(
            Utf8JsonWriter writer,
            TorrentStatus value,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(TorrentStatusExtn.Print(value));
    }
}