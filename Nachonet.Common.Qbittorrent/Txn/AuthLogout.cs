namespace Nachonet.Common.Qbittorrent.Txn
{
    public class AuthLogout(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider authenticationTokenProvider, string baseUri)
        : ApiCall<AuthLogoutRequest, AuthLogoutResponse>(httpClientFactory, authenticationTokenProvider, baseUri)
    {
        public override async Task<AuthLogoutResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await AuthLogoutResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}