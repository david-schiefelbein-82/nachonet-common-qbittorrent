using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{

    public class SearchResultsResult
    {
        [JsonPropertyName("descrLink")]
        public string DescrLink { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        [JsonPropertyName("fileSize")]
        public long FileSize { get; set; }

        [JsonPropertyName("fileUrl")]
        public string FileUrl { get; set; }

        [JsonPropertyName("nbLeechers")]
        public int Leechers { get; set; }

        [JsonPropertyName("nbSeeders")]
        public int Seeders { get; set; }

        public SearchResultsResult()
        {
            DescrLink = string.Empty;
            FileName = string.Empty;
            FileSize = 0;
            FileUrl = string.Empty;
            Leechers = 0;
            Seeders = 0;
        }
    }

    public class SearchResultsResponse : IResponse
    {
        [JsonPropertyName("results")]
        public SearchResultsResult[] Results { get; set; }

        public SearchResultsResponse()
        {
            Results = Array.Empty<SearchResultsResult>();
        }

        public override string ToString()
        {
            return Response.Print(this, true);
        }
    }
}