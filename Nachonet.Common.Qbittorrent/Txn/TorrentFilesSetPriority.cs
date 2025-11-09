namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentFilesSetPriority : ApiCall<TorrentFilesSetPriorityRequest, TorrentFilesSetPriorityResponse>
    {
        public TorrentFilesSetPriority(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : base(httpClientFactory, authenticationTokenProvider, baseUri)
        {
        }

        public override async Task<TorrentFilesSetPriorityResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await TorrentFilesSetPriorityResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}