using Nachonet.Common.Qbittorrent.Txn;
using System.Net;

namespace Nachonet.Common.Qbittorrent
{
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        public event EventHandler<AuthenticationTokenEventArgs>? AuthenticationTokenGenerated;

        /// <summary>
        /// represents the active-task for logging in
        /// </summary>
        private Task<AuthenticationToken>? _activeTask;

        public AuthenticationToken? AuthenticationToken { get; private set; }

        public Func<CancellationToken, Task<AuthenticationToken>>? RefreshAsync { get; set; }

        public AuthenticationTokenProvider()
        {
        }


        public void Expire()
        {
            lock (this)
            {
                AuthenticationToken = null;
            }
        }

        private async Task<AuthenticationToken> RefreshNowAsync(CancellationToken cancellationToken = default)
        {
            if (RefreshAsync == null)
                throw new QbittorrentAuthException(HttpStatusCode.Forbidden, "user not logged in");

            return await RefreshAsync.Invoke(cancellationToken);
        }

        public async Task<AuthenticationToken> GetTokenAsync(TokenOptions options = TokenOptions.None, CancellationToken cancellationToken = default)
        {
            AuthenticationToken? token;
            Task<AuthenticationToken> activeTask;
            bool startedLocally = false;

            // lock and retrieve our instance data into local fields
            lock (this)
            {
                token = AuthenticationToken;
                if (token != null && options != TokenOptions.Regenerate)
                    return token;

                if (options == TokenOptions.DontRegenerateIfMissing)
                    throw new QbittorrentAuthException(HttpStatusCode.Forbidden, "user not logged in");

                if (_activeTask == null)
                {
                    // no action running, run now
                    startedLocally = true;
                    _activeTask = RefreshNowAsync(cancellationToken);
                }

                // copy it locally
                activeTask = _activeTask;
            }

            try
            {
                // unlock and wait for the active action to complete
                token = await activeTask;
            }
            catch
            {
                // a fault occurred, clear out the activerequest
                lock (this)
                {
                    _activeTask = null;
                }

                // and exit with failure
                throw;
            }

            // lock again and assign the result
            lock (this)
            {
                _activeTask = null;
                AuthenticationToken = token;
            }

            if (startedLocally)
                AuthenticationTokenGenerated?.Invoke(this, new AuthenticationTokenEventArgs(token));

            return token;
        }
    }
}
