using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentDeleteRequest : IRequest
    {
        [JsonPropertyName("hashes")]
        public string[] Hashes { get; set; }

        [JsonPropertyName("deleteFiles")]
        public bool DeleteFiles { get; set; }

        public TorrentDeleteRequest(string[] hashes, bool deleteFiles)
        {
            Hashes = hashes;
            DeleteFiles = deleteFiles;
        }

        public string Name => "torrents/delete";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("hashes", string.Join('|', Hashes)),
                new KeyValuePair<string, string>("deleteFiles", DeleteFiles ? "true" : "false"),
            };

            return new HttpRequestMessage(HttpMethod.Post, baseUri + "/api/v2/" + Name)
            {
                Content = new FormUrlEncodedContent(param),
            };
        }
    }
}