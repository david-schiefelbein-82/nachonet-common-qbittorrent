using Nachonet.Common.Qbittorrent.Txn;

namespace Nachonet.Common.Qbittorrent
{
    public interface IQbittorrentClient
    {
        event EventHandler<AuthenticationTokenEventArgs>? AuthenticationTokenGenerated;

        Task<AuthLoginResponse> AuthLoginAsync(AuthLoginRequest request, CancellationToken cancellationToken = default);

        Task<AuthLogoutResponse> AuthLogoutAsync(AuthLogoutRequest request, CancellationToken cancellationToken = default);

        Task<AppVersionResponse> AppVersion(AppVersionRequest appVersionRequest, CancellationToken cancellationToken = default);

        Task<SearchResultsResponse> InternalSearchAsync(SearchStartRequest request, CancellationToken cancellationToken = default);

        Task<SearchStartResponse> SearchStartAsync(SearchStartRequest request, CancellationToken cancellationToken = default);

        Task<SearchStatusResponse> SearchStatusAsync(SearchStatusRequest request, CancellationToken cancellationToken = default);

        Task<SearchResultsResponse> SearchResultsAsync(SearchResultsRequest request, CancellationToken cancellationToken = default);

        Task<SearchPluginsResponse> SearchPluginsAsync(SearchPluginsRequest request, CancellationToken cancellationToken = default);

        Task<TorrentAddResponse> TorrentAddAsync(TorrentAddRequest request, CancellationToken cancellationToken = default);

        Task<TorrentDeleteResponse> TorrentDeleteAsync(TorrentDeleteRequest request, CancellationToken cancellationToken = default);

        Task<TorrentPauseResponse> TorrentPauseAsync(TorrentPauseRequest request, CancellationToken cancellationToken = default);

        Task<TorrentResumeResponse> TorrentResumeAsync(TorrentResumeRequest request, CancellationToken cancellationToken = default);

        Task<TorrentListResponse> TorrentListAsync(TorrentListRequest request, CancellationToken cancellationToken = default);

        Task<TorrentFilesResponse> TorrentFilesAsync(TorrentFilesRequest request, CancellationToken cancellationToken = default);

        Task<TorrentFilesSetPriorityResponse> TorrentFilesSetPriorityAsync(TorrentFilesSetPriorityRequest request, CancellationToken cancellationToken = default);
    }
}