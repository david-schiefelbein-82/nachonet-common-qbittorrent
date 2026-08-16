namespace Nachonet.Common.Qbittorrent.Txn
{
    public class SearchStartRequest : IRequest
    {
        public string Pattern { get; set; }
        public string? Plugins { get; set; }
        public string? Category { get; set; }

        public SearchStartRequest(string pattern, string? plugins = null, string? category = null)
        {
            Pattern = pattern;
            Plugins = plugins;
            Category = category;
        }

        public string Name => "search/start";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var httpReq = new HttpRequestMessage(HttpMethod.Post, baseUri + "/api/v2/" + Name);
            var param = new List<KeyValuePair<string, string>>
            {
                new("pattern", Pattern),
                new("plugins", Plugins ?? "all"),
                new("category", Category ?? "all")
            };

            httpReq.Content = new FormUrlEncodedContent(param);
            return httpReq;
        }
    }
}