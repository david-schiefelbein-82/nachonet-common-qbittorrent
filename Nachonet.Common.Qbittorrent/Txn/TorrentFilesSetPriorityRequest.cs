using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentFilesSetPriorityRequest : IRequest
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("ids")]
        public int[] Ids { get; set; }

        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        public TorrentFilesSetPriorityRequest(string hash, int[] ids, int priority)
        {
            Hash = hash;
            Ids = ids;
            Priority = priority;
        }

        public string Name => "torrents/filePrio";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("hash", Hash),
                new KeyValuePair<string, string>("id", string.Join('|', Ids)),
                new KeyValuePair<string, string>("priority", Priority.ToString()),
            };

            var query = new FormUrlEncodedContent(param).ReadAsStringAsync().Result;

            return new HttpRequestMessage(HttpMethod.Post, baseUri + "/api/v2/" + Name)
            {
                Content = new FormUrlEncodedContent(param),
            };
        }
    }
}