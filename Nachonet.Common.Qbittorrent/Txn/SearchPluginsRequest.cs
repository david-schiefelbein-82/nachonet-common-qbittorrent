using System.Text.Json.Serialization;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class SearchPluginsRequest : IRequest
    {
        public SearchPluginsRequest()
        {
        }

        public string Name => "search/plugins";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            return new HttpRequestMessage(HttpMethod.Get, baseUri + "/api/v2/" + Name);
        }
    }
}