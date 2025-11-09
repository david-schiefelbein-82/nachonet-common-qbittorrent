using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class SearchStatusRequest : IRequest
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        public SearchStatusRequest(long id)
        {
            Id = id;
        }

        public string Name => "search/status";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            IList<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", Id.ToString()),
            };

            string query = new FormUrlEncodedContent(param).ReadAsStringAsync().Result;
            return new HttpRequestMessage(HttpMethod.Get, baseUri + "/api/v2/" + Name + "?" + query);
        }
    }
}