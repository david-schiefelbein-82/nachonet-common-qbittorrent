using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public enum TorrentStatus
    {
        /// <summary>
        /// Unknown status
        /// </summary>
        Unknown,

        /// <summary>
        /// Some error occurred, applies to paused torrents
        /// </summary>
        Error,

        /// <summary>
        /// Torrent data files is missing
        /// </summary>
        MissingFiles,

        /// <summary>
        /// Torrent is being seeded and data is being transferred
        /// </summary>
        Uploading,

        /// <summary>
        /// Torrent is paused and has finished downloading
        /// </summary>
        PausedUP,

        /// <summary>
        /// Queuing is enabled and torrent is queued for upload
        /// </summary>
        QueuedUP,

        /// <summary>
        /// Torrent is being seeded, but no connection were made
        /// </summary>
        StalledUP,
        /// <summary>
        /// Torrent has finished downloading and is being checked
        /// </summary>
        CheckingUP,

        /// <summary>
        /// Torrent is forced to uploading and ignore queue limit
        /// </summary>
        ForcedUP,

        /// <summary>
        /// Torrent is allocating disk space for download
        /// </summary>
        Allocating,

        /// <summary>
        /// Torrent is being downloaded and data is being transferred
        /// </summary>
        Downloading,

        /// <summary>
        /// Torrent has just started downloading and is fetching metadata
        /// </summary>
        MetaDL,

        /// <summary>
        /// Torrent is paused and has NOT finished downloading
        /// </summary>
        PausedDL,

        /// <summary>
        /// Queuing is enabled and torrent is queued for download
        /// </summary>
        QueuedDL,

        /// <summary>
        /// Torrent is being downloaded, but no connection were made
        /// </summary>
        StalledDL,

        /// <summary>
        /// Same as checkingUP, but torrent has NOT finished downloading
        /// </summary>
        CheckingDL,

        /// <summary>
        /// Torrent is forced to downloading to ignore queue limit
        /// </summary>
        ForcedDL,

        /// <summary>
        /// Checking resume data on qBt startup
        /// </summary>
        CheckingResumeData,

        /// <summary>
        /// Torrent is moving to another location
        /// </summary>
        Moving,
    }

    public static class TorrentStatusExtn
    {
        public static string Print(TorrentStatus value)
        {
            return value switch
            {
                TorrentStatus.Unknown => "unknown",
                TorrentStatus.Error => "error",
                TorrentStatus.MissingFiles => "missingFiles",
                TorrentStatus.Uploading => "uploading",
                TorrentStatus.PausedUP => "pausedUP",
                TorrentStatus.QueuedUP => "queuedUP",
                TorrentStatus.StalledUP => "stalledUP",
                TorrentStatus.CheckingUP => "checkingUP",
                TorrentStatus.ForcedUP => "forcedUP",
                TorrentStatus.Allocating => "allocating",
                TorrentStatus.Downloading => "downloading",
                TorrentStatus.MetaDL => "metaDL",
                TorrentStatus.PausedDL => "pausedDL",
                TorrentStatus.QueuedDL => "queuedDL",
                TorrentStatus.StalledDL => "stalledDL",
                TorrentStatus.CheckingDL => "checkingDL",
                TorrentStatus.ForcedDL => "forcedDL",
                TorrentStatus.CheckingResumeData => "checkingResumeData",
                TorrentStatus.Moving => "moving",
                _ => value.ToString(),
            };
        }

        public static TorrentStatus Parse(string value)
        {
            if (int.TryParse(value, out var iresult))
            {
                return (TorrentStatus)iresult;
            }

            if (Enum.TryParse(value, true, out TorrentStatus result))
            {
                return result;
            }

            return value switch
            {
                "unknown" => TorrentStatus.Unknown,
                "error" => TorrentStatus.Error,
                "missingFiles" => TorrentStatus.MissingFiles,
                "uploading" => TorrentStatus.Uploading,
                "pausedUP" => TorrentStatus.PausedUP,
                "queuedUP" => TorrentStatus.QueuedUP,
                "stalledUP" => TorrentStatus.StalledUP,
                "checkingUP" => TorrentStatus.CheckingUP,
                "forcedUP" => TorrentStatus.ForcedUP,
                "allocating" => TorrentStatus.Allocating,
                "downloading" => TorrentStatus.Downloading,
                "metaDL" => TorrentStatus.MetaDL,
                "pausedDL" => TorrentStatus.PausedDL,
                "queuedDL" => TorrentStatus.QueuedDL,
                "stalledDL" => TorrentStatus.StalledDL,
                "checkingDL" => TorrentStatus.CheckingDL,
                "forcedDL" => TorrentStatus.ForcedDL,
                "checkingResumeData" => TorrentStatus.CheckingResumeData,
                "moving" => TorrentStatus.Moving,
                _ => throw new Exception("cannot parse " + value + " into TorrentStatus"),
            };
        }
    }
}