using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nachonet.Common.Qbittorrent.Test
{
    public class TestBase
    {
        public static string User
        {
            get
            {
                var user = Environment.GetEnvironmentVariable("QBittorrent-User");
                if (user == null)
                {
                    return "dave";
                }

                return user;
            }
        }

        public static string Password
        {
            get
            {
                var pwd = Environment.GetEnvironmentVariable("QBittorrent-Password");
                return pwd ?? throw new Exception("QBittorrent-Password is not set");
            }
        }

        public static string BaseUrl
        {
            get
            {
                var url = Environment.GetEnvironmentVariable("QBittorrent-BaseURL");
                return url ?? throw new Exception("QBittorrent-BaseURL is not set");
            }
        }
    }
}
