namespace Nachonet.Common.Qbittorrent.Txn
{
    public class AuthLoginRequest(string userName, string password) : IRequest
    {
        public string UserName { get; set; } = userName;

        public string Password { get; set; } = password;

        public string Name => "auth/login";

        public HttpRequestMessage ToRequest(string baseUri)
        {
            var httpReq = new HttpRequestMessage(HttpMethod.Post, baseUri + "/api/v2/auth/login");

            IList<KeyValuePair<string, string>> param =
            [
                new("username", UserName),
                new("password", Password)
            ];
            httpReq.Content = new FormUrlEncodedContent(param);
            return httpReq;
        }

    }
}