namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentDelete : ApiCall<TorrentDeleteRequest, TorrentDeleteResponse>
    {
        public TorrentDelete(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : base(httpClientFactory, authenticationTokenProvider, baseUri)
        {
        }

        public override async Task<TorrentDeleteResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await TorrentDeleteResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}