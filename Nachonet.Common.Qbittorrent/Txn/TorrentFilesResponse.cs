using System.Text.Json.Serialization;
using Nachonet.Common.Qbittorrent.Converters;

namespace Nachonet.Common.Qbittorrent.Txn
{
    /// <summary>
    /// [{"availability":1,"index":0,"is_seed":false,"name":"ubuntu-22.04-desktop-amd64.iso","piece_range":[0,13942],"priority":1,"progress":0.9123865952202312,"size":3654957056}]
    /// </summary>
    public class TorrentFile
    {

        /// <summary>
        /// Percentage of file pieces currently available(percentage/100)
        /// </summary>
        [JsonPropertyName("availability")]
        public double Availability { get; set; }

        /// <summary>
        /// File index since 2.8.2
        /// </summary>
        [JsonPropertyName("index")]
        public long Index { get; set; }

        /// <summary>
        /// True if file is seeding/complete
        /// </summary>
        [JsonPropertyName("is_seed")]
        public bool IsSeed { get; set; }

        /// <summary>
        /// File name (including relative path)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The first number is the starting piece index and the second number is the ending piece index (inclusive)
        /// </summary>
        [JsonPropertyName("piece_range")]
        public long[] PieceRange { get; set; }

        /// <summary>
        /// File priority. possible values:
        /// 0	Do not download
        /// 1	Normal priority
        /// 6	High priority
        /// 7	Maximal priority
        /// </summary>
        [JsonPropertyName("priority")]
        public long Priority { get; set; }

        /// <summary>
        /// File progress (percentage/100)
        /// </summary>
        [JsonPropertyName("progress")]
        public double Progress { get; set; }

        /// <summary>
        /// File size (bytes)
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }

        public TorrentFile()
        {
            Name = string.Empty;
            PieceRange = [0, 0];
        }
    }

    public class TorrentFilesResponse(TorrentFile[] results) : IResponse
    {
        public TorrentFile[] Results { get; set; } = results;

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<TorrentFilesResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);

            var results = Response.Parse<TorrentFile[]>(msg);
            return new TorrentFilesResponse(results);
        }
    }
}