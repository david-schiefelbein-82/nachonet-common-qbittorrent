using Nachonet.Common.Qbittorrent.Txn;

namespace Nachonet.Common.Qbittorrent
{
    public class QbittorrentClient : IQbittorrentClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IQbittorrentClientConfig _config;
        private readonly AuthenticationTokenProvider _authProvider;

        public event EventHandler<AuthenticationTokenEventArgs>? AuthenticationTokenGenerated;

        public QbittorrentClient(IHttpClientFactory httpClientFactory, IQbittorrentClientConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _authProvider = new();
            _authProvider.AuthenticationTokenGenerated += OnAuthenticationTokenGenerated;
        }

        protected virtual void OnAuthenticationTokenGenerated(object? sender, AuthenticationTokenEventArgs ev)
        {
            AuthenticationTokenGenerated?.Invoke(this, ev);
        }

        public async Task<AuthLoginResponse> AuthLoginAsync(AuthLoginRequest request, CancellationToken cancellationToken = default)
        {
            _authProvider.RefreshAsync = new Func<CancellationToken, Task<AuthenticationToken>>(async (refreshToken) =>
            {
                var refreshMethod = new AuthLogin(_httpClientFactory, _config.BaseUrl);
                var refreshResponse = await refreshMethod.ExecuteAsync(request, refreshToken);
                return refreshResponse.AuthenticationToken;
            });

            var token = await _authProvider.GetTokenAsync(TokenOptions.Regenerate, cancellationToken);
            return new AuthLoginResponse(token);
        }

        public async Task<AuthLogoutResponse> AuthLogoutAsync(AuthLogoutRequest request, CancellationToken cancellationToken = default)
        {
            var method = new AuthLogout(_httpClientFactory, _authProvider, _config.BaseUrl);
            var result = await method.ExecuteAsync(request, cancellationToken);
            _authProvider.Expire();
            return result;
        }

            public async Task<AppVersionResponse> AppVersion(AppVersionRequest request, CancellationToken cancellationToken = default)
        {
            var method = new AppVersion(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<SearchResultsResponse> InternalSearchAsync(SearchStartRequest request, CancellationToken cancellationToken = default)
        {
            var method = new InternalSearch(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<SearchStartResponse> SearchStartAsync(SearchStartRequest request, CancellationToken cancellationToken = default)
        {
            var method = new SearchStart(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<SearchStatusResponse> SearchStatusAsync(SearchStatusRequest request, CancellationToken cancellationToken = default)
        {
            var method = new SearchStatus(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<SearchResultsResponse> SearchResultsAsync(SearchResultsRequest request, CancellationToken cancellationToken = default)
        {
            var method = new SearchResults(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<SearchPluginsResponse> SearchPluginsAsync(SearchPluginsRequest request, CancellationToken cancellationToken = default)
        {
            var method = new SearchPlugins(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<TorrentAddResponse> TorrentAddAsync(TorrentAddRequest request, CancellationToken cancellationToken = default)
        {
            var method = new TorrentAdd(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<TorrentDeleteResponse> TorrentDeleteAsync(TorrentDeleteRequest request, CancellationToken cancellationToken = default)
        {
            var method = new TorrentDelete(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<TorrentPauseResponse> TorrentPauseAsync(TorrentPauseRequest request, CancellationToken cancellationToken = default)
        {
            var method = new TorrentPause(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<TorrentResumeResponse> TorrentResumeAsync(TorrentResumeRequest request, CancellationToken cancellationToken = default)
        {
            var method = new TorrentResume(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<TorrentListResponse> TorrentListAsync(TorrentListRequest request, CancellationToken cancellationToken = default)
        {
            var method = new TorrentList(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<TorrentFilesResponse> TorrentFilesAsync(TorrentFilesRequest request, CancellationToken cancellationToken = default)
        {
            var method = new TorrentFiles(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }

        public async Task<TorrentFilesSetPriorityResponse> TorrentFilesSetPriorityAsync(TorrentFilesSetPriorityRequest request, CancellationToken cancellationToken = default)
        {
            var method = new TorrentFilesSetPriority(_httpClientFactory, _authProvider, _config.BaseUrl);
            return await method.ExecuteAsync(request, cancellationToken);
        }
    }
}
