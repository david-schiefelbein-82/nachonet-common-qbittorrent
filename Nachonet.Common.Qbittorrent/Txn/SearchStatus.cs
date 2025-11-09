namespace Nachonet.Common.Qbittorrent.Txn
{
    public class SearchStatus : ApiCall<SearchStatusRequest, SearchStatusResponse>
    {
        public SearchStatus(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : base(httpClientFactory, authenticationTokenProvider, baseUri)
        {
        }

        public override async Task<SearchStatusResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await SearchStatusResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}