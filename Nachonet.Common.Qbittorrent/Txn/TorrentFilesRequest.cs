using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentFilesRequest : IRequest
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        public TorrentFilesRequest(string hash)
        {
            Hash = hash;
        }

        public string Name => "torrents/files";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("hash", Hash),
            };

            var query = new FormUrlEncodedContent(param).ReadAsStringAsync().Result;

            return new HttpRequestMessage(HttpMethod.Get, baseUri + "/api/v2/" + Name + "?" + query);
        }
    }
}