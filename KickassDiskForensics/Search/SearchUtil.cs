using System;
using System.Collections.Generic;
using System.Text;
using KFS.DataStream;

namespace KFA.Search {
    class SearchUtil {

        public delegate bool MatchFunction(Byte[] bytes);
        public delegate void ProgressCallback(ulong location);

        public static ulong[] simpleFindAll(IDataStream stream, byte[] search) {
            List<ulong> all = new List<ulong>();
            ulong l = SimpleFind(stream, search, 0, null);
            while (l != UInt64.MaxValue) {
                all.Add(l);
                l = SimpleFind(stream, search, l, null);
            }
            return all.ToArray();
        }

        public static ulong FindHexString(IDataStream stream, String searchString, ulong start, ProgressCallback callback) {
            searchString = searchString.Replace(" ", "");
            Byte[] search = new Byte[searchString.Length / 2];
            for (int i = 0; i < search.Length; i++) {
                search[i] = Byte.Parse(searchString.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return FindBytes(stream, search, start, callback);
        }

        public static ulong FindTextAscii(IDataStream stream, String searchString, ulong start, ProgressCallback callback) {
            return FindText(stream, searchString, Encoding.ASCII, start, callback);
        }

        public static ulong FindTextUnicode(IDataStream stream, String searchString, ulong start, ProgressCallback callback) {
            return FindText(stream, searchString, Encoding.Unicode, start, callback);
        }

        public static ulong FindText(IDataStream stream, String searchString, Encoding encoding, ulong start, ProgressCallback callback) {
            return FindBytes(stream, encoding.GetBytes(searchString), start, callback);
        }

        public static ulong FindBytes(IDataStream stream, byte[] search, ulong start, ProgressCallback callback) {
            return SimpleFind(stream, search, start, callback);
        }

        private static ulong SimpleFind(IDataStream stream, byte[] search, ulong start, ProgressCallback callback) {
            for (ulong l = start; l < stream.StreamLength - (ulong)search.Length; l+=1) {
                if (callback != null && l % 1024 == 0) {
                    callback(l);
                }
                bool found = true;
                for (int m = 0; m < search.Length; m++) {
                    if (search[m] != stream.GetByte((ulong) m + l)) {
                        found = false;
                        break;
                    }
                }
                if (found) {
                    return l;
                }
            }
            return UInt64.MaxValue;
        }

        /*private static ulong simpleFindCluster(IDataStream stream, MatchFunction function, ulong start) {
            for (ulong l = start; l < stream.StreamLength - (ulong)search.Length; l += 1) {
                bool found = true;
                for (int m = 0; m < search.Length; m++) {
                    if (search[m] != stream.GetByte((ulong)m + l)) {
                        found = false;
                        break;
                    }
                }
                if (found) {
                    return l;
                }
            }
            return UInt64.MaxValue;
        }*/
    }
}
