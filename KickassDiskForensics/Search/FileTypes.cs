using System;
using System.Collections.Generic;
using System.Linq;
using KFA.DataStream;
using FileSystems.FileSystem;

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
            return stream.StreamLength > 4 &&
                ((stream.GetByte(0) == 'I' && stream.GetByte(1) == 'I')
                 || (stream.GetByte(0) == 'M' && stream.GetByte(1) == 'M'))
                 && Util.GetUInt16(stream, 2) == 42;
        }

        private static bool IsGif(IDataStream stream) {
            return stream.StreamLength > 6
                && stream.GetByte(0) == 'G'
                && stream.GetByte(1) == 'I'
                && stream.GetByte(2) == 'F';
        }

        private static bool IsJpeg(IDataStream stream) {
            return stream.StreamLength > 2
                && stream.GetByte(0) == 0xFF
                && stream.GetByte(1) == 0xD8;
        }

        private static bool IsPng(IDataStream stream) {
            return stream.StreamLength > 8
                && stream.GetByte(0) == 0x89
                && stream.GetByte(1) == 0x50
                && stream.GetByte(2) == 0x4E
                && stream.GetByte(3) == 0x47
                && stream.GetByte(4) == 0x0D
                && stream.GetByte(5) == 0x0A
                && stream.GetByte(6) == 0x1A
                && stream.GetByte(7) == 0x0A;
        }

        private static bool IsBmp(IDataStream stream) {
            return stream.StreamLength > 2
                && stream.GetByte(0) == 0x42
                && stream.GetByte(1) == 0x4D;
        }
    }
}
