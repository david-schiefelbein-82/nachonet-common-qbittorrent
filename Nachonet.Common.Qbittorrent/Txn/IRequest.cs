namespace Nachonet.Common.Qbittorrent.Txn
{
    public interface IRequest
    {
        string Name { get; }

        HttpRequestMessage ToRequest(string baseUri);
    }
}