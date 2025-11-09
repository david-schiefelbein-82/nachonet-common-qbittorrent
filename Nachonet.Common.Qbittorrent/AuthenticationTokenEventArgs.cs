using Nachonet.Common.Qbittorrent.Txn;

namespace Nachonet.Common.Qbittorrent
{
    public class AuthenticationTokenEventArgs(AuthenticationToken authenticationToken) : EventArgs
    {
        public AuthenticationToken AuthenticationToken { get; } = authenticationToken;
    }
}