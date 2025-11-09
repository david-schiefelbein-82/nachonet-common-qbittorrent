using Nachonet.Common.Qbittorrent.Converters;
using System.Text.Json;

namespace Nachonet.Common.Qbittorrent.Test
{
    [TestClass]
    public class TestUnixNullableEpocConverter
    {

        [TestMethod]
        public void Read()
        {
            var options = new JsonSerializerOptions();
            var converter = new UnixEpocNullableConverter();
            var bytes = System.Text.Encoding.UTF8.GetBytes("1712115924");
            var reader = new Utf8JsonReader(bytes);
            reader.Read();
            DateTime? value = converter.Read(ref reader, typeof(DateTime), options);
            var expected = new DateTime(2024, 4, 3, 3, 45, 24, DateTimeKind.Utc).ToLocalTime();
            Assert.AreEqual(expected, value);
        }

        [TestMethod]
        public void ReadNull()
        {
            var options = new JsonSerializerOptions();
            var converter = new UnixEpocNullableConverter();
            var bytes = System.Text.Encoding.UTF8.GetBytes("null");
            var reader = new Utf8JsonReader(bytes);
            reader.Read();
            DateTime? value = converter.Read(ref reader, typeof(DateTime), options);
            var expected = new DateTime(2024, 4, 3, 3, 45, 24, DateTimeKind.Utc).ToLocalTime();
            Assert.AreEqual(expected, value);
        }

        [TestMethod]
        public void Write()
        {
            var options = new JsonSerializerOptions();
            var converter = new UnixEpocConverter();

            var timeStamp = new DateTime(2024, 4, 3, 10, 5, 6, DateTimeKind.Local);
            var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            converter.Write(writer, timeStamp, options);
            writer.Flush();
            var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            Assert.AreEqual("1712099106", json);
        }

        [TestMethod]
        public void WriteNull()
        {
            var options = new JsonSerializerOptions();
            var converter = new UnixEpocNullableConverter();

            var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            converter.Write(writer, null, options);
            writer.Flush();
            var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            Assert.AreEqual("null", json);
        }

        [TestMethod]
        public void WriteUtc()
        {
            var options = new JsonSerializerOptions();
            var converter = new UnixEpocConverter();

            var timeStamp = new DateTime(2024, 4, 2, 23, 5, 6, DateTimeKind.Utc);
            var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);
            converter.Write(writer, timeStamp, options);
            writer.Flush();
            var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            Assert.AreEqual("1712099106", json);
        }
    }
}