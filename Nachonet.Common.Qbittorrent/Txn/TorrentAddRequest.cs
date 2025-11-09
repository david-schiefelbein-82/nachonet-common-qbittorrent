using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentAddRequest : IRequest
    {
        [JsonPropertyName("urls")]
        public string TorrentUri { get; set; }

        public TorrentAddRequest(string torrentUri)
        {
            TorrentUri = torrentUri;
        }

        public string Name => "torrents/add";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("urls", TorrentUri),
            };

            return new HttpRequestMessage(HttpMethod.Post, baseUri + "/api/v2/" + Name)
            {
                Content = new FormUrlEncodedContent(param)
            };
        }
    }
}