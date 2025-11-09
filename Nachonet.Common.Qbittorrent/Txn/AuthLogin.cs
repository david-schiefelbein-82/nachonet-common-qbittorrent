namespace Nachonet.Common.Qbittorrent.Txn
{
    public class AuthLogin(IHttpClientFactory httpClientFactory, string baseUri) : ApiCall<AuthLoginRequest, AuthLoginResponse>(httpClientFactory, null, baseUri)
    {
        public override async Task<AuthLoginResponse> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            return await AuthLoginResponse.ParseAsync(httpResp, cancellationToken);
        }
    }
}