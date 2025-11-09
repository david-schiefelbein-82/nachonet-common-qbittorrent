using System.Net;
using System.Runtime.CompilerServices;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class AppVersionResponse(string version) : IResponse
    {
        public string Version { get; } = version;

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<AppVersionResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);
            return new AppVersionResponse(msg);
        }
    }
}