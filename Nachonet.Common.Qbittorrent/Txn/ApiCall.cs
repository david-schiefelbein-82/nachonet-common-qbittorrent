using System.Net.Http;
using System.Text.Json;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public interface IApiCall<TReq, TResp>
        where TReq : IRequest
        where TResp : IResponse
    {

        public Task<TResp> ExecuteAsync(TReq request, CancellationToken cancellationToken = default);
    }

    public abstract class ApiCall<TReq, TResp>(IHttpClientFactory httpClientFactory, IAuthenticationTokenProvider? authenticationTokenProvider, string baseUri) : IApiCall<TReq, TResp>
        where TReq : IRequest
        where TResp : IResponse
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IAuthenticationTokenProvider? _authenticationTokenProvider = authenticationTokenProvider;
        private readonly string _baseUri = baseUri;

        public virtual async Task<TResp> ExecuteAsync(TReq request, CancellationToken cancellationToken = default)
        {
            return await ExecuteAsync(request, 1, cancellationToken);
        }

        public virtual async Task<TResp> ExecuteAsync(TReq request, int attempt, CancellationToken cancellationToken = default)
        {
            using var client = _httpClientFactory.CreateClient(request.Name);
            var httpReq = request.ToRequest(_baseUri);
            if (_authenticationTokenProvider != null)
            {
                var options = TokenOptions.None;
                if (request is AuthLogoutRequest)
                {
                    options = TokenOptions.DontRegenerateIfMissing;
                }
                else if (request is AuthLoginRequest)
                {
                    options = TokenOptions.Regenerate;
                }

                var authenticationToken = await _authenticationTokenProvider.GetTokenAsync(options, cancellationToken);
                httpReq.Headers.Add("Cookie", authenticationToken.Cookie);

                try
                {
                    var httpResp = await client.SendAsync(httpReq, cancellationToken);
                    return await GetResponseAsync(httpResp, cancellationToken);
                }
                catch (QbittorrentAuthException)
                {
                    // we tried to perform an operation but the session cookie is bad
                    _authenticationTokenProvider.Expire();
                    if (attempt > 1 || request is AuthLoginRequest || request is AuthLogoutRequest)
                    {
                        // either we've already attempted this twice (attempt 1 -> login -> attempt 2)
                        // or it's a Login or Logout transaction and we don't login just to perform it
                        throw;
                    }

                    // try a second time
                    return await ExecuteAsync(request, attempt + 1, cancellationToken);
                }
            }
            else
            {
                // if we don't have an authentication provide then don't retry
                var httpResp = await client.SendAsync(httpReq, cancellationToken);
                return await GetResponseAsync(httpResp, cancellationToken);
            }
        }

        public virtual async Task<TResp> GetResponseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);

            return JsonSerializer.Deserialize<TResp>(msg)
                ?? throw new QbittorrentException(httpResp.StatusCode, "unable to decode " + typeof(TResp).FullName);
        }
    }

    public static class ApiHelper
    {
        public static void AssertSuccessStatusCode(this HttpResponseMessage httpResp, string msg)
        {
            if (!httpResp.IsSuccessStatusCode)
            {
                if (httpResp.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new QbittorrentAuthException(httpResp.StatusCode, string.Format("{0} {1} - {2}", httpResp.StatusCode, httpResp.ReasonPhrase, msg));
                }

                throw new QbittorrentException(httpResp.StatusCode, string.Format("{0} {1} - {2}", httpResp.StatusCode, httpResp.ReasonPhrase, msg));
            }
        }
    }
}