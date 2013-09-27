using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KFA.DataStream;
using FileSystems.FileSystem;

namespace KFA.Search {
    public class SearchResults : Folder {
        private List<FileSystemNode> children;
        private string name;

        public SearchResults(String name, List<FileSystemNode> children) {
            this.children = children;
            this.name = name;
        }

        public override IEnumerable<FileSystemNode> GetChildren() {
            return children;
        }

        public override String ToString() {
            return name;
        }

        public override byte GetByte(ulong offset) {
            return 0;
        }

        public override byte[] GetBytes(ulong offset, ulong length) {
            return new byte[length];
        }

        public override ulong DeviceOffset {
            get { return 0; }
        }

        public override ulong StreamLength {
            get {
                return 0;
            }
        }

        public override string StreamName {
            get {
                return "Search Results";
            }
        }

        public override IDataStream ParentStream {
            get { return null; }
        }

        public override void Open() {
        }

        public override void Close() {
        }

        public override long Identifier
        {
            get { return 0; }
        }

        public override DateTime LastModified
        {
            get { return DateTime.Now; }
        }
    }
}
