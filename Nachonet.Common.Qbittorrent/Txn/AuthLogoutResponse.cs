using System.Net;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class AuthLogoutResponse : IResponse
    {

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<AuthLogoutResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);
            return new AuthLogoutResponse();
        }
    }
}