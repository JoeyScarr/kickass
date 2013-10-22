using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using KFS.DataStream;
using KFS.FileSystems;
using System.Diagnostics;

namespace KFA.ApplicationLevel.History {
    /* File Locations:
     * CACHE: %systemdir%\Documents and Settings\%username%\Local Settings\Temporary Internet Files\Content.ie5
     * COOKIES: %systemdir%\Documents and Settings\%username%\Cookies
     * HISTORY: %systemdir%\Documents and Settings\%username%\Local Settings\History\history.ie5
     */
    public enum ExplorerHistoryType {
        None = 0,
        Cookie,
        Cache,
        MainHistory,
        DailyWeeklyHistory
    }
    public class ExplorerHistoryRecord {
        private static Regex m_MsHist = new Regex("^:[0-9]{16}: ", RegexOptions.Compiled);
        public DateTime LastAccessed {
            get { return LastAccessedUTC.ToLocalTime(); }
        }
        public DateTime LastModified {
            get { return LastModifiedUTC.ToLocalTime(); }
        }
        public DateTime LastAccessedUTC { get; private set; }
        public DateTime LastModifiedUTC { get; private set; }
        private string m_Description;
        public string Description {
            get {
                return m_Description;
            }
            private set {
                m_Description = value;
                if (m_Description.StartsWith("Visited: ")) {
                    m_Description = m_Description.Substring("Visited: ".Length);
                } else if (m_MsHist.Match(m_Description).Success) {
                    m_Description = m_Description.Substring(19);
                }
            }
        }
        private IDataStream m_DataStream = null;
        private FileSystemNode m_Folder = null;
        private string m_Filename = null;
        public ExplorerHistoryRecord(string desc, DateTime lastAccessed, DateTime lastModified, Folder folder, string filename) {
            Description = desc;
            LastAccessedUTC = lastAccessed;
            LastModifiedUTC = lastModified;
            if (LastAccessedUTC.Kind != DateTimeKind.Utc) Debugger.Break();
            if (LastModifiedUTC.Kind != DateTimeKind.Utc) Debugger.Break();
            m_Folder = folder;
            m_Filename = filename;
        }
        public ExplorerHistoryRecord(string desc, DateTime lastAccessed, DateTime lastModified, FileSystem fileSystem) {
            Description = desc;
            LastAccessedUTC = lastAccessed;
            LastModifiedUTC = lastModified;
            if (LastAccessedUTC.Kind != DateTimeKind.Utc) Debugger.Break();
            if (LastModifiedUTC.Kind != DateTimeKind.Utc) Debugger.Break();
            // Parse the description to get m_Filename
            Regex r = new Regex("/[c-zC-Z]:(?<filename>.*)$", RegexOptions.Compiled);
            Match m = r.Match(Description);
            if (!m.Success) {
                m_Folder = null;
            } else {
                m_Folder = fileSystem.GetRoot();
                m_Filename = HttpUtility.UrlDecode(m.Groups["filename"].Value);
            }
        }
        public IDataStream DataStream {
            get {
                if (m_DataStream == null && m_Folder != null && m_Filename != null) {
                    foreach (FileSystemNode node in m_Folder.GetChildrenAtPath(m_Filename)) {
                        m_DataStream = node;
                        break;
                    }
                }
                return m_DataStream;
            }
        }
    }
    public class ExplorerHistoryFile {
        private const ulong HEADER_SIZE = 27;
        private const ulong BLOCK_SIZE = 128;
        private List<ExplorerHistoryRecord> m_Records;
        public IEnumerable<ExplorerHistoryRecord> GetRecords() {
            foreach (ExplorerHistoryRecord rec in m_Records) {
                yield return rec;
            }
        }
        public ExplorerHistoryType Type { get; private set; }
        public string Path { get; private set; }
        public IDataStream Stream { get; private set; }
        public ExplorerHistoryFile(File file, Folder parent, ExplorerHistoryType type) {
            m_Records = new List<ExplorerHistoryRecord>();
            Path = file.Path;
            Stream = file;
            if (file != null) {
                string headerStr = Util.GetASCIIString(file, 0, HEADER_SIZE);
                if (headerStr == "Client UrlCache MMF Ver 5.2") {
                    Type = type;

                    // Read cache directories
                    List<Folder> cachedirs = new List<Folder>();
                    ulong cache_offset = 0x50;
                    string cache = Util.GetASCIIString(file, cache_offset, Util.StrLen(file, cache_offset, 8));
                    while (cache != "") {
                        foreach (FileSystemNode node in parent.GetChildren(cache)) {
                            if (node is Folder) {
                                cachedirs.Add(node as Folder);
                                break;
                            }
                        }
                        cache_offset += 12;
                        cache = Util.GetASCIIString(file, cache_offset, Util.StrLen(file, cache_offset, 8));
                    }

                    ulong offset = 0x4000;
                    while (offset < file.StreamLength) {
                        uint numBlocks = 1;
                        if (Util.GetASCIIString(file, offset, 4) == "URL ") {
                            numBlocks = Util.GetUInt32(file, offset + 4);
                            uint desc_offset = Util.GetUInt32(file, offset + 0x34);
                            uint cachedname_offset = Util.GetUInt32(file, offset + 0x3C);
                            DateTime lastAccessed;
                            DateTime lastModified;
                            GetDateTimeFields(out lastAccessed, out lastModified, file, offset + 8);
                            string desc = Util.GetASCIIString(file, offset + desc_offset, Util.StrLen(file, offset + desc_offset, 0x4000));
                            string cachedname = Util.GetASCIIString(file, offset + cachedname_offset, Util.StrLen(file, offset + cachedname_offset, 0x4000));
                            int cachenum = Util.GetByte(file, offset + 0x38);
                            if (cachedirs.Count > cachenum) {
                                m_Records.Add(new ExplorerHistoryRecord(desc, lastAccessed, lastModified, cachedirs[cachenum], cachedname));
                            } else {
                                m_Records.Add(new ExplorerHistoryRecord(desc, lastAccessed, lastModified, file.FileSystem));
                            }
                        }
                        offset += numBlocks * BLOCK_SIZE;
                    }
                }
            }
        }

        private void GetDateTimeFields(out DateTime lastAccessed, out DateTime lastModified, File file, ulong p) {
            long field1 = Util.GetInt64(file, p);
            long field2 = Util.GetInt64(file, p + 8);
            switch (Type) {
                case ExplorerHistoryType.Cache:
                    lastModified = DateTime.FromFileTimeUtc(field1);
                    lastAccessed = DateTime.FromFileTimeUtc(field2);
                    break;
                case ExplorerHistoryType.Cookie:
                    lastModified = DateTime.FromFileTimeUtc(field1);
                    lastAccessed = DateTime.FromFileTimeUtc(field2);
                    break;
                case ExplorerHistoryType.MainHistory:
                    lastModified = lastAccessed 
                        = DateTime.FromFileTimeUtc(field1);
                    break;
                case ExplorerHistoryType.DailyWeeklyHistory:
                    lastModified = lastAccessed
                        = new DateTime(DateTime.FromFileTimeUtc(field1).Ticks, DateTimeKind.Local).ToUniversalTime();
                    break;
                default:
                    lastModified = DateTime.FromFileTimeUtc(field1);
                    lastAccessed = DateTime.FromFileTimeUtc(field2);
                    break;
            }
        }
    }
}
