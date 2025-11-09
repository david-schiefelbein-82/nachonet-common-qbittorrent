using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class SearchStartResponse : IResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        public SearchStartResponse()
        {
            Id = 0;
        }

        public override string ToString()
        {
            return Response.Print(this, true);
        }
    }
}