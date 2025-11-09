using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
#pragma warning disable CS9113 // Parameter is unread.
    public class TorrentListRequest(string? filter = null) : IRequest
#pragma warning restore CS9113 // Parameter is unread.
    {
        [JsonPropertyName("urls")]
        public string Filter { get; set; } = "all";

        public string Name => "torrents/info";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new("filter", Filter),
            };

            var query = new FormUrlEncodedContent(param).ReadAsStringAsync().Result;

            return new HttpRequestMessage(HttpMethod.Get, baseUri + "/api/v2/" + Name + "?" + query);
        }
    }
}