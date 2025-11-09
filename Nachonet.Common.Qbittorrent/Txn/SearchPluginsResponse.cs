using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class SearchCategory
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public SearchCategory()
        {
            Id = string.Empty;
            Name = string.Empty;
        }
    }

    public class SearchPlugin
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("supportedCategories")]
        public SearchCategory[]? SupportedCategories { get; set; }

        public SearchPlugin()
        {
            Enabled = false;
            FullName = string.Empty;
            Name = string.Empty;
            Url = string.Empty;
            Version = string.Empty;
            SupportedCategories = null;
        }
    }

    public class SearchPluginsResponse(SearchPlugin[] result) : IResponse
    {
        public SearchPlugin[] Result { get; set; } = result;

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<SearchPluginsResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);

            return new SearchPluginsResponse(
                Response.Parse<SearchPlugin[]>(msg) ?? throw new QbittorrentException(httpResp.StatusCode, "unable to decode SearchStatusResponse"));
        }
    }
}