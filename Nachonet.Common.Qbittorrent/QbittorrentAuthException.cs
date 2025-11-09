using System.Net;

namespace Nachonet.Common.Qbittorrent
{
    public class QbittorrentAuthException : QbittorrentException
    {
        public QbittorrentAuthException(HttpStatusCode statusCode, string? message) : base(statusCode, message)
        {
        }

        public QbittorrentAuthException(HttpStatusCode statusCode, string? message, Exception? innerException) : base(statusCode, message, innerException)
        {
        }
    }
}
