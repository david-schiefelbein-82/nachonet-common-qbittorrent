namespace Nachonet.Common.Qbittorrent.Txn
{
    public class SearchPlugins : ApiCall<SearchPluginsRequest, SearchPluginsResponse>
    {
        public SearchPlugins(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : base(httpClientFactory, authenticationTokenProvider, baseUri)
        {
        }

        public override async Task<SearchPluginsResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await SearchPluginsResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}