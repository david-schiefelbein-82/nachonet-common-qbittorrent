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
            IList<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("pattern", Pattern),
                new KeyValuePair<string, string>("plugins", Plugins ?? "all"),
                new KeyValuePair<string, string>("category", Category ?? "all")
            };

            httpReq.Content = new FormUrlEncodedContent(param);
            return httpReq;
        }
    }
}