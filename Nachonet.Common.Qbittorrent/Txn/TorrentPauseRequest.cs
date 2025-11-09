using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentPauseRequest(string[] hashes) : IRequest
    {
        [JsonPropertyName("hashes")]
        public string[] Hashes { get; set; } = hashes;

        public string Name => "torrents/pause";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new("hashes", string.Join('|', Hashes)),
            };

            return new HttpRequestMessage(HttpMethod.Post, baseUri + "/api/v2/" + Name)
            {
                Content = new FormUrlEncodedContent(param),
            };
        }
    }
}