using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nachonet.Common.Qbittorrent
{
    public class QbittorrentClientConfig : IQbittorrentClientConfig
    {
        public string BaseUrl { get; }

        public QbittorrentClientConfig(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
    }
}
