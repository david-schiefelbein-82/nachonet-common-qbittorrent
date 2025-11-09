using Nachonet.Common.Qbittorrent.Txn;

namespace Nachonet.Common.Qbittorrent
{
    public enum TokenOptions
    {
        /// <summary>
        /// Normal operation, use existing token if provided, or request a new one from the Web API
        /// </summary>
        None = 0,
        /// <summary>
        /// Dont regenerate if missing.  For example on the logout operation we don't want to login 
        /// just to logout again
        /// </summary>
        DontRegenerateIfMissing = 0x1,
        /// <summary>
        /// Always regenerate even if there is an existing token.  For example a login operation we want
        /// to ensure it was a success, not just use a previous token
        /// </summary>
        Regenerate = 0x2
    }

    public interface IAuthenticationTokenProvider
    {
        event EventHandler<AuthenticationTokenEventArgs>? AuthenticationTokenGenerated;

        void Expire();

        Task<AuthenticationToken> GetTokenAsync(TokenOptions options = TokenOptions.None, CancellationToken cancellationToken = default);
    }
}