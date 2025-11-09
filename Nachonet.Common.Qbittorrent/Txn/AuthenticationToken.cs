namespace Nachonet.Common.Qbittorrent.Txn
{
    using System;

    public class AuthenticationToken(string cookie)
    {
        public string Cookie { get; } = cookie;

        public override string ToString()
        {
            return "Cookie: " + Cookie;
        }
    }
}
