namespace Nachonet.Common.Qbittorrent.Test
{
    using System.Net;
    using Nachonet.Common.Qbittorrent;
    using Nachonet.Common.Qbittorrent.Txn;

    [TestClass]
    public class TestAuth : TestBase
    {
        [TestMethod]
        public async Task TestVersion()
        {
            var authFactory = new MockHttpClientFactory(HttpStatusCode.OK, new StringContent("Ok."));
            var cancellationToken = default(CancellationToken);
            var authTokenProvider = new AuthenticationTokenProvider
            {
                RefreshAsync = async (token) =>
                {
                    var login = new AuthLogin(authFactory, BaseUrl);
                    var loginResponse = await login.ExecuteAsync(new AuthLoginRequest(User, Password), cancellationToken);
                    return loginResponse.AuthenticationToken;
                }
            };

            var appVersionFactory = new MockHttpClientFactory(HttpStatusCode.OK, new StringContent("v4.6.1"));
            var appVersionResponse = await new AppVersion(appVersionFactory, authTokenProvider, BaseUrl)
                .ExecuteAsync(new AppVersionRequest(), cancellationToken);

            Assert.AreEqual("v4.6.1", appVersionResponse.Version);
        }

        [TestMethod]
        [ExpectedException(typeof(QbittorrentAuthException), "Fails.")]
        public async Task TestBadLogin()
        {
            var authFactory = new MockHttpClientFactory(HttpStatusCode.OK, new StringContent("Fails."));
            var cancellationToken = default(CancellationToken);
            var login = new AuthLogin(authFactory, BaseUrl);
            await login.ExecuteAsync(new AuthLoginRequest("inccorrectUser", "inccorectPassword"), cancellationToken);
        }
    }
}