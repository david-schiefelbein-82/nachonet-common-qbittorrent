namespace Nachonet.Common.Qbittorrent.Test
{
    [TestClass]
    public class TestUri : TestBase
    {

        [TestMethod]
        public async Task TestUri1()
        {
            var param = new List<KeyValuePair<string, string>>
            {
                new("id", 43.ToString()),
            };

            var c = new FormUrlEncodedContent(param);
            var s = await c.ReadAsStringAsync();
            Assert.IsNotNull(s);
        }
    }
}