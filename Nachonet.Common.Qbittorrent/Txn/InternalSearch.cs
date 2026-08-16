using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class InternalSearch(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : ApiCall<SearchStartRequest, SearchResultsResponse>(httpClientFactory, authenticationTokenProvider, baseUri)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public override async Task<SearchResultsResponse> ExecuteAsync(SearchStartRequest request, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
        {
            using var client = _httpClientFactory.CreateClient("internal-search");

            IList<KeyValuePair<string, string>> param =
            [
                new KeyValuePair<string, string>("q", request.Pattern),
                new KeyValuePair<string, string>("cat", request.Category ?? "0"),
            ];

            if (timeout.HasValue)
                client.Timeout = timeout.Value;

            string query = await new FormUrlEncodedContent(param).ReadAsStringAsync(cancellationToken);
            var httpResp = await client.GetAsync("https://apibay.org/q.php?" + query);

            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);

            var tpbResults = JsonSerializer.Deserialize<ThePirateBayResult[]>(msg)
                ?? throw new QbittorrentException(httpResp.StatusCode, "unable to decode " + typeof(ThePirateBayResult).FullName);
            var results = from x in tpbResults
                          select new SearchResultsResult()
                     {
                         FileName = x.Name,
                         DescrLink = x.Imdb,
                         FileSize = long.Parse(x.Size),
                         FileUrl = string.Format("magnet:?xt=urn:btih:{0}&dn={1}&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969%2Fannounce&tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A6969%2Fannounce&tr=udp%3A%2F%2F9.rarbg.to%3A2710%2Fannounce&tr=udp%3A%2F%2F9.rarbg.me%3A2780%2Fannounce&tr=udp%3A%2F%2F9.rarbg.to%3A2730%2Fannounce&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337&tr=http%3A%2F%2Fp4p.arenabg.com%3A1337%2Fannounce&tr=udp%3A%2F%2Ftracker.torrent.eu.org%3A451%2Fannounce&tr=udp%3A%2F%2Ftracker.tiny-vps.com%3A6969%2Fannounce&tr=udp%3A%2F%2Fopen.stealth.si%3A80%2Fannounce"
                            , x.InfoHash, Quote(x.Name)),
                         Leechers = int.Parse(x.Leechers),
                         Seeders = int.Parse(x.Seeders),
                     };
            return new SearchResultsResponse() { Results = [.. results] };
        }

        private static string Quote(string name)
        {
            return HttpUtility.UrlEncode(name);
        }

        public class ThePirateBayResult
        {

            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("info_hash")]
            public string InfoHash { get; set; }

            [JsonPropertyName("leechers")]
            public string Leechers { get; set; }

            [JsonPropertyName("seeders")]
            public string Seeders { get; set; }

            [JsonPropertyName("num_files")]
            public string NumFiles { get; set; }

            [JsonPropertyName("size")]
            public string Size { get; set; }

            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("added")]
            public string Added { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("category")]
            public string Category { get; set; }

            [JsonPropertyName("imdb")]
            public string Imdb { get; set; }

            public override string ToString()
            {
                return $"{{ id: {Id}, name: {Name} }}"; 
            }

            public ThePirateBayResult()
            {
                Id = string.Empty;
                Name = string.Empty;
                InfoHash = string.Empty;
                Leechers = string.Empty;
                Seeders = string.Empty;
                NumFiles = string.Empty;
                Size = string.Empty;
                Username = string.Empty;
                Added = string.Empty;
                Status = string.Empty;
                Category = string.Empty;
                Imdb = string.Empty;
            }
        }
    }
}