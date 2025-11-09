using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public enum SearchStatusCode
    {
        Unknown,
        Running,
        Stopped,
    }

    public class SearchStatusResult
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SearchStatusCode Status { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    public class SearchStatusResponse(SearchStatusResult[] result) : IResponse
    {
        public SearchStatusResult[] Result { get; set; } = result;

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<SearchStatusResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);
            return new SearchStatusResponse(
                Response.Parse<SearchStatusResult[]>(msg) ?? throw new QbittorrentException(httpResp.StatusCode, "unable to decode SearchStatusResponse"));
        }
    }
}