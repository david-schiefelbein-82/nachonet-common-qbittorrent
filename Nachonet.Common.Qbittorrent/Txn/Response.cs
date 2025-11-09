using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class Response
    {
        private static readonly JsonSerializerOptions _serialiserOpts = new() { WriteIndented = false };
        private static readonly JsonSerializerOptions _serialiserOptsIndented = new() { WriteIndented = true };

        public static T Parse<T>(string msg)
        {
            return JsonSerializer.Deserialize<T>(msg) ?? throw new QbittorrentException(HttpStatusCode.OK, "unable to decode SearchStatusResponse");
        }

        public static string Print<T>(T response, bool writeIndented)
        {
            return JsonSerializer.Serialize(response, writeIndented ? _serialiserOptsIndented : _serialiserOpts);
        }
    }
}