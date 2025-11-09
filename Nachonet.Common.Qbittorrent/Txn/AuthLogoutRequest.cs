namespace Nachonet.Common.Qbittorrent.Txn
{
    public class AuthLogoutRequest() : IRequest
    {
        public string Name => "auth/logout";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var httpReq = new HttpRequestMessage(HttpMethod.Post, baseUri + "/api/v2/" + Name);
            return httpReq;
        }

    }
}