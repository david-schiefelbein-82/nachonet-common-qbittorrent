namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentList(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : ApiCall<TorrentListRequest, TorrentListResponse>(httpClientFactory, authenticationTokenProvider, baseUri)
    {
        public override async Task<TorrentListResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await TorrentListResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}