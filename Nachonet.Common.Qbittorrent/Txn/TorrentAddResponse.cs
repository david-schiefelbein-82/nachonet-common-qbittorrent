namespace Nachonet.Common.Qbittorrent.Txn
{

    public class TorrentAddResponse : IResponse
    {

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<TorrentAddResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);

            if (!msg.StartsWith("OK", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new QbittorrentException(httpResp.StatusCode, msg);
            }

            return new TorrentAddResponse();
        }
    }
}