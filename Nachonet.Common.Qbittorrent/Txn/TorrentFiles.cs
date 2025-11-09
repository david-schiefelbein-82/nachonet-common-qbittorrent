namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentFiles : ApiCall<TorrentFilesRequest, TorrentFilesResponse>
    {
        public TorrentFiles(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : base(httpClientFactory, authenticationTokenProvider, baseUri)
        {
        }

        public override async Task<TorrentFilesResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await TorrentFilesResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}