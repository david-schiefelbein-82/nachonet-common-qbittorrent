namespace Nachonet.Common.Qbittorrent.Txn
{

    public class TorrentDeleteResponse : IResponse
    {

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<TorrentDeleteResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);
            return new TorrentDeleteResponse();
        }
    }
}