namespace Nachonet.Common.Qbittorrent.Txn
{
    public class SearchStart : ApiCall<SearchStartRequest, SearchStartResponse>
    {
        public SearchStart(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : base(httpClientFactory, authenticationTokenProvider, baseUri)
        {
        }
    }
}