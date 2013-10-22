using System;
using System.Collections.Generic;
using System.Linq;
using KFS.DataStream;
using KFS.FileSystems;

namespace KFA.Search {
    public enum FileType {
        All,
        Documents,
        Images,
        Audio,
        Video,
        WebPages,
        Scripts,
        PrintSpoolFiles,
        Unknown
    }
    public static class FileTypes {
        private static readonly Dictionary<FileType, HashSet<string>> m_Extensions = new Dictionary<FileType, HashSet<string>>() {
            {FileType.Documents, new HashSet<string>() {"doc","xls","docx","xlsx","txt","rtf","pdf"}},
            {FileType.Images, new HashSet<string>() {"jpg","jpeg","png","bmp","gif","ico","tif", "tiff"}},
            {FileType.Audio, new HashSet<string>() {"wav","mp3","wma"}},
            {FileType.Video, new HashSet<string>() {"avi","mkv","wmv","mp4"}},
            {FileType.WebPages, new HashSet<string>() {"html","htm","php","asp","aspx"}},
            {FileType.Scripts, new HashSet<string>() {"bat","js"}},
            {FileType.PrintSpoolFiles, new HashSet<string>() {"spl"}}
        };
        private static readonly Dictionary<FileType, Predicate<IDataStream>> m_HeaderChecks = new Dictionary<FileType, Predicate<IDataStream>>() {
            {FileType.Images, IsPicture}
        };
        public static Dictionary<FileType, HashSet<string>> SupportedFileTypes {
            get { return new Dictionary<FileType, HashSet<string>>(m_Extensions); }
        }
        public static bool IsFileType(File file, FileType type) {
            if (type == FileType.All) return true;

            if (!string.IsNullOrEmpty(file.Name)) {
                string[] bits = file.Name.ToLower().Split('.');
                if (bits.Length > 0 && m_Extensions[type].Contains(bits.Last())) {
                    return true;
                }
            }
            if (m_HeaderChecks.ContainsKey(type) && m_HeaderChecks[type](file)) {
                return true;
            }
            return false;
        }
        public static FileType GetFileType(File file) {
            foreach (FileType type in m_Extensions.Keys) {
                if (IsFileType(file, type)) {
                    return type;
                }
            }
            return FileType.Unknown;
        }
        public static bool IsPicture(IDataStream stream) {
            stream.Open();
            bool res = IsJpeg(stream) || IsPng(stream) || IsBmp(stream) || IsGif(stream) || IsTif(stream);
            stream.Close();
            return res;
        }

        private static bool IsTif(IDataStream stream) {
					if (stream.StreamLength > 4) {
						byte[] header = stream.GetBytes(0, 4);
						return ((header[0] == 'I' && header[1] == 'I')
							|| (header[0] == 'M' && header[1] == 'M'))
							&& BitConverter.ToUInt16(header, 2) == 42;
					}
					return false;
        }

        private static bool IsGif(IDataStream stream) {
					if (stream.StreamLength > 6) {
						byte[] header = stream.GetBytes(0, 3);
						return header[0] == 'G' && header[1] == 'I' && header[2] == 'F';
					}
					return false;
        }

				private static bool IsJpeg(IDataStream stream) {
					if (stream.StreamLength > 2) {
						byte[] header = stream.GetBytes(0, 2);
						return header[0] == 0xFF && header[1] == 0xD8;
					}
					return false;
        }

				private static bool IsPng(IDataStream stream) {
					if (stream.StreamLength > 8) {
						byte[] header = stream.GetBytes(0, 8);
						return header[0] == 0x89
								&& header[1] == 0x50
								&& header[2] == 0x4E
								&& header[3] == 0x47
								&& header[4] == 0x0D
								&& header[5] == 0x0A
								&& header[6] == 0x1A
								&& header[7] == 0x0A;
					}
					return false;
        }

				private static bool IsBmp(IDataStream stream) {
					if (stream.StreamLength > 2) {
						byte[] header = stream.GetBytes(0, 2);
						return header[0] == 0x42 && header[1] == 0x4D;
					}
					return false;
        }
    }
}
