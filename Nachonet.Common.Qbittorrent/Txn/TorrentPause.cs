namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentPause(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : ApiCall<TorrentPauseRequest, TorrentPauseResponse>(httpClientFactory, authenticationTokenProvider, baseUri)
    {
        public override async Task<TorrentPauseResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await TorrentPauseResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}