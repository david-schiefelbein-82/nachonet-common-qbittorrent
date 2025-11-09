using System.Net.Http;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class AppVersion(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri) : ApiCall<AppVersionRequest, AppVersionResponse>(httpClientFactory, authenticationTokenProvider, baseUri)
    {
        public override async Task<AppVersionResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await AppVersionResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}