namespace Nachonet.Common.Qbittorrent.Txn
{
    public class SearchResults : ApiCall<SearchResultsRequest, SearchResultsResponse>
    {
        public SearchResults(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : base(httpClientFactory, authenticationTokenProvider, baseUri)
        {
        }
    }
}