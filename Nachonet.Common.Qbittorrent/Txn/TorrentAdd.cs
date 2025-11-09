namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentAdd : ApiCall<TorrentAddRequest, TorrentAddResponse>
    {
        public TorrentAdd(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : base(httpClientFactory, authenticationTokenProvider, baseUri)
        {
        }

        public override async Task<TorrentAddResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await TorrentAddResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}