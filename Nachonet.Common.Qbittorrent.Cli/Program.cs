using Microsoft.Extensions.DependencyInjection;
using Nachonet.Common.Qbittorrent.Txn;

namespace Nachonet.Common.Qbittorrent.Cli
{
    internal class Program
    {
        public static readonly StringComparison cmp = StringComparison.CurrentCultureIgnoreCase;

        static void Main(string[] args)
        {
            string baseUrl = "http://localhost:8080";
            for (int i = 0; i < args.Length - 1; ++i)
            {
                if (args[i].Equals("-baseUrl", cmp))
                {
                    baseUrl = args[++i];
                }
            }


            var services = new ServiceCollection();
            services.AddSingleton<IQbittorrentClientConfig>(new QbittorrentClientConfig(baseUrl));
            services.AddSingleton<IQbittorrentClient, QbittorrentClient>();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();
            IQbittorrentClient client = serviceProvider.GetRequiredService<IQbittorrentClient>();
            var cancellationToken = default(CancellationToken);

            var task = RunAsync(client, cancellationToken);
            task.Wait();
        }

        private static async Task RunAsync(IQbittorrentClient client, CancellationToken cancellationToken = default)
        {
            long searchId = 0;
            string? topUrl = null;
            string? hash = null;

            while (true)
            {
                Console.Write("Qbittorrent> ");
                var cmd = CommandUtils.ParseCommand(Console.ReadLine(), out string[] args);

                try
                {
                    if (cmd == Command.Exit)
                    {
                        break;
                    }
                    else if (cmd == Command.Login)
                    {
                        var user = PromptIfMissing("user", args, 1);
                        var pass = PromptIfMissing("pass", args, 2);
                        var loginResponse = await client.AuthLoginAsync(new AuthLoginRequest(user, pass), cancellationToken);
                        Console.WriteLine(cmd + " : " + loginResponse.ToString());
                    }
                    else if (cmd == Command.Logout)
                    {
                        var logoutResponse = await client.AuthLogoutAsync(new AuthLogoutRequest(), cancellationToken);
                        Console.WriteLine(cmd + " : " + logoutResponse.ToString());
                    }
                    else if (cmd == Command.InternalSearch)
                    {
                        var pattern = PromptIfMissing("Search Pattern", args, 1);

                        if (args.Length == 0)
                        {
                            Console.Error.WriteLine("Search: Missing Pattern argument.  Try \"Search <pattern>\"");
                            continue;
                        }

                        var response = await client.InternalSearchAsync(new SearchStartRequest(pattern), cancellationToken);
                        var topResult = response.Results.FirstOrDefault();
                        topUrl = topResult?.FileUrl;
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.Search)
                    {
                        var pattern = PromptIfMissing("Search Pattern", args, 1);
                        var response = await client.SearchStartAsync(new SearchStartRequest(pattern), cancellationToken);
                        searchId = response.Id;
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.SearchStatus)
                    {
                        var sId = Prompt("Search ID", searchId);
                        var response = await client.SearchStatusAsync(new SearchStatusRequest(sId), cancellationToken);
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.SearchResults)
                    {
                        var sId = Prompt("Search ID", searchId);
                        var response = await client.SearchResultsAsync(new SearchResultsRequest(sId), cancellationToken);
                        var topResult = response.Results.FirstOrDefault();
                        topUrl = topResult?.FileUrl;
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.SearchPlugins)
                    {
                        var response = await client.SearchPluginsAsync(new SearchPluginsRequest(), cancellationToken);
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.TorrentAdd)
                    {
                        var torrentUrl = Prompt("Torrent URL", topUrl ?? string.Empty);
                        if (string.IsNullOrEmpty(topUrl))
                        {
                            Console.WriteLine("Error - URL required");
                            continue;
                        }

                        var response = await client.TorrentAddAsync(new TorrentAddRequest(torrentUrl), cancellationToken);
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.TorrentDelete)
                    {
                        var torrentHash = Prompt("hash", hash ?? string.Empty);
                        if (string.IsNullOrEmpty(torrentHash))
                        {
                            Console.WriteLine("Error - hash required");
                            continue;
                        }

                        var response = await client.TorrentDeleteAsync(new TorrentDeleteRequest([torrentHash], false), cancellationToken);
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.TorrentPause)
                    {
                        var torrentHash = Prompt("hash", hash ?? string.Empty);
                        if (string.IsNullOrEmpty(torrentHash))
                        {
                            Console.WriteLine("Error - hash required");
                            continue;
                        }

                        var response = await client.TorrentPauseAsync(new TorrentPauseRequest([torrentHash]), cancellationToken);
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.TorrentResume)
                    {
                        var torrentHash = Prompt("hash", hash ?? string.Empty);
                        if (string.IsNullOrEmpty(torrentHash))
                        {
                            Console.WriteLine("Error - hash required");
                            continue;
                        }

                        var response = await client.TorrentResumeAsync(new TorrentResumeRequest([torrentHash]), cancellationToken);
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.AppVersion)
                    {

                        var response = await client.AppVersion(new AppVersionRequest(), cancellationToken);
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.TorrentList)
                    {
                        var response = await client.TorrentListAsync(new TorrentListRequest(), cancellationToken);
                        var topResult = response.Results.FirstOrDefault();
                        hash = topResult?.Hash;
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.TorrentFiles)
                    {
                        var torrentHash = Prompt("hash", hash ?? string.Empty);
                        var response = await client.TorrentFilesAsync(new TorrentFilesRequest(torrentHash), cancellationToken);
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.TorrentFilesSetPriority)
                    {
                        var torrentHash = Prompt("hash", hash ?? string.Empty);
                        var id = PromptInt("id", 0);
                        int priority = PromptInt("priority 0,1,6,7", 0);
                        var response = await client.TorrentFilesSetPriorityAsync(new TorrentFilesSetPriorityRequest(torrentHash, [id], priority), cancellationToken);
                        Console.WriteLine(cmd + " : " + response.ToString());
                    }
                    else if (cmd == Command.Help)
                    {
                        Console.WriteLine("Qbittorrent cli commands");
                        Console.WriteLine("Login            - login with a user and password");
                        Console.WriteLine("Logout           - logout");
                        Console.WriteLine("AppVersion       - gets the version of the server");
                        Console.WriteLine("InternalSearch {pattern} - start a search for the pattern (required) using internal API (not qbittorrent)");
                        Console.WriteLine("Search {pattern} - start a search for the pattern (required)");
                        Console.WriteLine("SearchStatus     - get the state of the search");
                        Console.WriteLine("SearchResults    - list the search results");
                        Console.WriteLine("TorrentAdd       - add the first torrent from the search results");
                        Console.WriteLine("TorrentDelete    - remove the first torrent from {TorrentList}");
                        Console.WriteLine("TorrentList      - list all torrents");
                        Console.WriteLine("TorrentFiles     - remove the first torrent from {TorrentList}");
                    }
                    else
                    {
                        if (args.Length > 0)
                        {
                            Console.WriteLine("Unknown command " + string.Join(" ", args));
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("{0}: {1}", ex.GetType(), ex.Message));
                }
            }
        }

        private static string PromptIfMissing(string prompt, string[] args, int index)
        {
            string parm = args.Length > index ? args[index] : string.Empty;

            if (string.IsNullOrEmpty(parm))
            {
                return Prompt(prompt, parm);
            }

            return parm;
        }

        private static long Prompt(string prompt, long @default)
        {
            while (true)
            {
                Console.Write(prompt + "? [" + @default + "] ");
                var ln = Console.ReadLine() ?? throw new Exception("Console Closed");
                if (string.IsNullOrWhiteSpace(ln))
                {
                    return @default;
                }

                if (long.TryParse(ln, out long result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("\"" + ln + "\" is not a number");
                    continue;
                }
            }
        }

        private static string Prompt(string prompt, string @default)
        {
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(@default))
                    Console.Write(prompt + "? [" + @default + "] ");
                else
                    Console.Write(prompt + "? ");

                var ln = Console.ReadLine() ?? throw new Exception("Console Closed");
                if (string.IsNullOrWhiteSpace(ln))
                {
                    ln = @default;
                }

                if (!string.IsNullOrWhiteSpace(ln))
                {
                    return ln;
                }
            }
        }

        private static int PromptInt(string prompt, int? @default)
        {
            while (true)
            {
                if (@default != null)
                    Console.Write(prompt + "? [" + @default + "] ");
                else
                    Console.Write(prompt + "? ");

                var ln = Console.ReadLine() ?? throw new Exception("Console Closed");
                if (@default != null && string.IsNullOrWhiteSpace(ln))
                {
                    return @default.Value;
                }

                if (!string.IsNullOrWhiteSpace(ln))
                {
                    if (int.TryParse(ln, out int result))
                    {
                        return result;
                    }
                }
            }
        }
    }
}