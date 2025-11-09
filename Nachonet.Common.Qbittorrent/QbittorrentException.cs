using System.Net;

namespace Nachonet.Common.Qbittorrent
{
    public class QbittorrentException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public QbittorrentException(HttpStatusCode statusCode, string? message) : base(message)
        {
            StatusCode = statusCode;
        }

        public QbittorrentException(HttpStatusCode statusCode, string? message, Exception? innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
