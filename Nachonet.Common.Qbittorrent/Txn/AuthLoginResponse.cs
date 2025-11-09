using System.Net;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class AuthLoginResponse : IResponse
    {
        public AuthenticationToken AuthenticationToken { get; }

        public AuthLoginResponse(string cookie)
        {
            AuthenticationToken = new AuthenticationToken(cookie);
        }
        public AuthLoginResponse(AuthenticationToken token)
        {
            AuthenticationToken = token;
        }

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<AuthLoginResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);

            if (!httpResp.IsSuccessStatusCode)
            {
                throw new QbittorrentAuthException(httpResp.StatusCode, string.Format("{0:d} {1} - {2}", httpResp.StatusCode, httpResp.ReasonPhrase, msg));
            }

            if (!msg.StartsWith("OK", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new QbittorrentAuthException(httpResp.StatusCode, msg);
            }

            var cookieHdr = (from x in httpResp.Headers
                             where x.Key.Equals("Set-Cookie", StringComparison.CurrentCultureIgnoreCase)
                             select x.Value).FirstOrDefault() ?? throw new QbittorrentException(HttpStatusCode.OK, "Missing Header Set-Cookie header");
            var cookieParts = cookieHdr.FirstOrDefault() ?? throw new QbittorrentAuthException(HttpStatusCode.OK, "Missing Header Set-Cookie header");
            var parts = cookieParts.Split(';');
            foreach (var part in parts)
            {
                var trimmedPart = part.Trim();
                if (trimmedPart.StartsWith("SID", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new AuthLoginResponse(trimmedPart);
                }
            }

            throw new QbittorrentAuthException(HttpStatusCode.OK, "Missing Header Set-Cookie: SID=");
        }
    }
}