namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentFilesSetPriorityResponse : IResponse
    {
        public TorrentFilesSetPriorityResponse()
        {
        }

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<TorrentFilesSetPriorityResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);

            return new TorrentFilesSetPriorityResponse();
        }
    }
}