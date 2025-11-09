using System.Net;

namespace Nachonet.Common.Qbittorrent.Test
{
    public class MockHttpClientFactory(HttpStatusCode statusCode, HttpContent content) : IHttpClientFactory
    {
        private readonly HttpStatusCode _statusCode = statusCode;
        private readonly HttpContent _content = content;

        public HttpClient CreateClient(string name)
        {
            return new HttpClient(new MockHttpMessageHandler(_statusCode, _content));
        }

        public class MockHttpMessageHandler(HttpStatusCode statusCode, HttpContent content) : HttpMessageHandler
        {
            private readonly HttpStatusCode _statusCode = statusCode;
            private readonly HttpContent _content = content;

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.Run(() =>
                {
                    return new HttpResponseMessage(_statusCode)
                    {
                        Content = _content,
                    };
                });
            }
        }
    }
}