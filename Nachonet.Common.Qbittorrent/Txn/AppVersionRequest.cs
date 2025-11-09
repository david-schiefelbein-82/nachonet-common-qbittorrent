namespace Nachonet.Common.Qbittorrent.Txn
{
    public class AppVersionRequest : IRequest
    {
        public AppVersionRequest()
        {
        }

        public string Name => "app/version";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var httpReq = new HttpRequestMessage(HttpMethod.Get, baseUri + "/api/v2/" + Name);
            return httpReq;
        }

    }
}