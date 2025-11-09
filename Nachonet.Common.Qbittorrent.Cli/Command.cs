using System.Text;

namespace Nachonet.Common.Qbittorrent.Cli
{
    public enum Command
    {
        Undefined,
        Exit,
        Login,
        Logout,
        Help,
        InternalSearch,
        Search,
        SearchStatus,
        SearchResults,
        SearchPlugins,
        TorrentAdd,
        TorrentDelete,
        TorrentList,
        TorrentFiles,
        TorrentFilesSetPriority,
        TorrentPause,
        TorrentResume,
        AppVersion,
    }

    public class CommandUtils
    {
        public static Command ParseCommand(string? cmd, out string[] args)
        {
            if (cmd == null)
            {
                args = [string.Empty];
                return Command.Exit;
            }

            args = Tokenise(cmd);
            if (args.Length > 0)
            {
                if (!Enum.TryParse(args[0], true, out Command command))
                {
                    command = Command.Undefined;
                }

                return command;
            }

            args = [string.Empty];
            return Command.Undefined;
        }

        private static string[] Tokenise(string cmdLine)
        {
            var args = new List<string>();
            if (string.IsNullOrWhiteSpace(cmdLine))
                return [.. args];

            var currentArg = new StringBuilder();
            bool inQuotedArg = false;

            for (int i = 0; i < cmdLine.Length; i++)
            {
                if (cmdLine[i] == '"')
                {
                    if (inQuotedArg)
                    {
                        args.Add(currentArg.ToString());
                        currentArg.Clear();
                        inQuotedArg = false;
                    }
                    else
                    {
                        inQuotedArg = true;
                    }
                }
                else if (cmdLine[i] == ' ')
                {
                    if (inQuotedArg)
                    {
                        currentArg.Append(cmdLine[i]);
                    }
                    else if (currentArg.Length > 0)
                    {
                        args.Add(currentArg.ToString());
                        currentArg.Clear();
                    }
                }
                else
                {
                    currentArg.Append(cmdLine[i]);
                }
            }

            if (currentArg.Length > 0)
                args.Add(currentArg.ToString());

            return [.. args];
        }
    }
}
