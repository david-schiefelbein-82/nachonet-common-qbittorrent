namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentResume(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : ApiCall<TorrentResumeRequest, TorrentResumeResponse>(httpClientFactory, authenticationTokenProvider, baseUri)
    {
        public override async Task<TorrentResumeResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await TorrentResumeResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}