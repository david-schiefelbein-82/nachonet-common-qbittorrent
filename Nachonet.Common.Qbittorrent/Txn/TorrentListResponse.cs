
using System.Net;
using System.Text.Json.Serialization;
using Nachonet.Common.Qbittorrent.Converters;

namespace Nachonet.Common.Qbittorrent.Txn
{
    public class TorrentInfo
    {
        /// <summary>
        /// Time (Unix Epoch) when the torrent was added to the client
        /// </summary>
        [JsonConverter(typeof(UnixEpocNullableConverter))]
        [JsonPropertyName("added_on")]
        public DateTime? AddedOn { get; set; }

        /// <summary>
        /// Amount of data left to download (bytes)
        /// </summary>
        [JsonPropertyName("amount_left")]
        public long AmmountLeft { get; set; }

        /// <summary>
        /// Whether this torrent is managed by Automatic Torrent Management
        /// </summary>
        [JsonPropertyName("auto_tmm")]
        public bool AutoTmm { get; set; }

        /// <summary>
        /// Percentage of file pieces currently available
        /// </summary>
        [JsonPropertyName("availability")]
        public double Availability { get; set; }

        /// <summary>
        /// Category of the torrent
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// Amount of transfer data completed (bytes)
        /// </summary>
        [JsonPropertyName("completed")]
        public long Completed { get; set; }

        /// <summary>
        /// Time (Unix Epoch) when the torrent completed
        /// </summary>
        [JsonPropertyName("completion_on")]
        [JsonConverter(typeof(UnixEpocNullableConverter))]
        public DateTime? CompletionOn { get; set; }

        /// <summary>
        /// Absolute path of torrent content (root path for multifile torrents, absolute file path for singlefile torrents)
        /// </summary>
        [JsonPropertyName("content_path")]
        public string ContentPath { get; set; }

        /// <summary>
        /// Torrent download speed limit (bytes/s). -1 if ulimited.
        /// </summary>
        [JsonPropertyName("dl_limit")]
        public long DownloadLimit { get; set; }

        /// <summary>
        /// Torrent download speed (bytes/s)
        /// </summary>
        [JsonPropertyName("dlspeed")]
        public long DownloadSpeed { get; set; }

        [JsonPropertyName("download_path")]
        public string DownloadPath { get; set; }

        /// <summary>
        /// Amount of data downloaded
        /// </summary>
        [JsonPropertyName("downloaded")]
        public long Downloaded { get; set; }

        /// <summary>
        /// Amount of data downloaded this session
        /// </summary>
        [JsonPropertyName("downloaded_session")]
        public long DownloadedSession { get; set; }

        /// <summary>
        /// Torrent ETA (seconds)
        /// </summary>
        [JsonPropertyName("eta")]
        public long Eta { get; set; }

        /// <summary>
        /// True if first last piece are prioritized
        /// </summary>
        [JsonPropertyName("f_l_piece_prio")]
        public bool FirstAndLastPiecePriotorised { get; set; }

        /// <summary>
        /// True if force start is enabled for this torrent
        /// </summary>
        [JsonPropertyName("force_start")]
        public bool ForceStart { get; set; }

        /// <summary>
        /// Torrent hash
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// </summary>
        [JsonPropertyName("inactive_seeding_time_limit")]
        public int InactiveSeedingTimeLimit { get; set; }

        [JsonPropertyName("infohash_v1")]
        public string InfohashV1 { get; set; }

        [JsonPropertyName("infohash_v2")]
        public string InfohashV2 { get; set; }

        /// <summary>
        /// Last time (Unix Epoch) when a chunk was downloaded/uploaded
        /// </summary>
        [JsonPropertyName("last_activity")]
        [JsonConverter(typeof(UnixEpocNullableConverter))]
        public DateTime? LastActivity { get; set; }

        /// <summary>
        /// Magnet URI corresponding to this torrent
        /// </summary>
        [JsonPropertyName("magnet_uri")]
        public string MagnetUri { get; set; }


        [JsonPropertyName("max_inactive_seeding_time")]
        public int MaxInactiveSeedingTime { get; set; }

        /// <summary>
        /// Get the global share ratio limit
        /// </summary>
        [JsonPropertyName("max_ratio")]
        public int MaxRatio { get; set; }

        /// <summary>
        /// Number of minutes to seed a torrent
        /// </summary>
        [JsonPropertyName("max_seeding_time")]
        public int MaxSeedingTime { get; set; }

        /// <summary>
        /// Number of seeds in the swarm
        /// </summary>
        [JsonPropertyName("num_complete")]
        public int NumComplete { get; set; }

        /// <summary>
        /// Name of the torrent
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Number of leechers in the swarm
        /// </summary>
        [JsonPropertyName("num_incomplete")]
        public int NumIncomplete { get; set; }

        /// <summary>
        /// Number of leechers connected to
        /// </summary>
        [JsonPropertyName("num_leechs")]
        public int NumLeechs { get; set; }

        /// <summary>
        /// Number of seeds connected to
        /// </summary>
        [JsonPropertyName("num_seeds")]
        public int NumSeeds { get; set; }

        /// <summary>
        /// Torrent priority. Returns -1 if queuing is disabled or torrent is in seed mode
        /// </summary>
        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        /// <summary>
        /// Torrent share ratio. Max ratio value: 9999.
        /// </summary>
        [JsonPropertyName("ratio")]
        public float Ratio { get; set; }

        /// <summary>
        /// Torrent progress (percentage/100)
        /// </summary>
        [JsonPropertyName("progress")]
        public float Progress { get; set; }

        /// <summary>
        /// TODO (what is different from max_ratio?)
        /// </summary>
        [JsonPropertyName("ratio_limit")]
        public long RatioLimit { get; set; }

        /// <summary>
        /// Path where this torrent's data is stored
        /// </summary>
        [JsonPropertyName("save_path")]
        public string SavePath { get; set; }

        /// <summary>
        /// Torrent elapsed time while complete (seconds)
        /// </summary>
        [JsonPropertyName("seeding_time")]
        public long SeedingTime { get; set; }

        /// <summary>
        /// TODO (what is different from max_seeding_time?) seeding_time_limit
        /// is a per torrent setting, when Automatic Torrent Management is disabled, 
        /// furthermore then max_seeding_time is set to seeding_time_limit for this 
        /// torrent. If Automatic Torrent Management is enabled, the value is -2. 
        /// And if max_seeding_time is unset it have a default value -1.
        /// </summary>
        [JsonPropertyName("seeding_time_limit")]
        public long SeedingTimeLimit { get; set; }

        /// <summary>
        /// Time (Unix Epoch) when this torrent was last seen complete
        /// </summary>
        [JsonPropertyName("seen_complete")]
        [JsonConverter(typeof(UnixEpocNullableConverter))]
        public DateTime? SeenComplete { get; set; }

        /// <summary>
        /// True if sequential download is enabled
        /// </summary>
        [JsonPropertyName("seq_dl")]
        public bool SequentialDownload { get; set; }

        /// <summary>
        /// Total size (bytes) of files selected for download
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }

        /// <summary>
        /// Torrent state. See <see cref="TorrentStatus"/> for the possible values
        /// </summary>
        [JsonPropertyName("state")]
        [JsonConverter(typeof(TorrentStatusConverter))]
        public TorrentStatus State { get; set; } // TODO

        [JsonPropertyName("super_seeding")]
        public bool SuperSeeding { get; set; }

        /// <summary>
        /// Comma-concatenated tag list of the torrent
        /// </summary>
        [JsonPropertyName("tags")]
        public string Tags { get; set; }

        /// <summary>
        /// Total active time (seconds)
        /// </summary>
        [JsonPropertyName("time_active")]
        public long TimeActive { get; set; }

        /// <summary>
        /// Total size (bytes) of all file in this torrent (including unselected ones)
        /// </summary>
        [JsonPropertyName("total_size")]
        public long TotalSize { get; set; }

        /// <summary>
        /// The first tracker with working status. Returns empty string if no tracker is working.
        /// </summary>
        [JsonPropertyName("tracker")]
        public string Tracker { get; set; }

        [JsonPropertyName("trackers_count")]
        public int TrackersCount { get; set; }

        /// <summary>
        /// Torrent upload speed limit (bytes/s). -1 if ulimited.
        /// </summary>
        [JsonPropertyName("up_limit")]
        public int UpoadLimit { get; set; }

        /// <summary>
        /// Amount of data uploaded
        /// </summary>
        [JsonPropertyName("uploaded")]
        public long Uploaded { get; set; }

        /// <summary>
        /// Amount of data uploaded this session
        /// </summary>
        [JsonPropertyName("uploaded_session")]
        public long UploadedSession { get; set; }

        /// <summary>
        /// Torrent upload speed (bytes/s)
        /// </summary>
        [JsonPropertyName("upspeed")]
        public long UploadSpeed { get; set; }

        public TorrentInfo()
        {
            Category = string.Empty;
            Hash = string.Empty;
            InfohashV1 = string.Empty;
            InfohashV2 = string.Empty;
            Name = string.Empty;
            MagnetUri = string.Empty;
            SavePath = string.Empty;
            Tracker = string.Empty;
            Tags = string.Empty;
            DownloadPath = string.Empty;
            ContentPath = string.Empty;
        }
    }


    public class TorrentListResponse(TorrentInfo[] results) : IResponse
    {
        public TorrentInfo[] Results { get; set; } = results;

        public override string ToString()
        {
            return Response.Print(this, true);
        }

        public static async Task<TorrentListResponse> ParseAsync(HttpResponseMessage httpResp, CancellationToken cancellationToken = default)
        {
            var msg = await httpResp.Content.ReadAsStringAsync(cancellationToken);
            httpResp.AssertSuccessStatusCode(msg);

            var results = Response.Parse<TorrentInfo[]>(msg);
            return new TorrentListResponse(results);
        }
    }
}