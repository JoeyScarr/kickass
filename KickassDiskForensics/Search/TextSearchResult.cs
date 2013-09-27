using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KFA.DataStream;
using FileSystems.FileSystem;
using KFA.GUI.Explorers;

namespace KFA.Search {
    class TextSearchResult : File, IClickable {
        private IDataStream m_stream;
        private ulong m_location, m_length;

        public TextSearchResult(IDataStream stream, ulong location, ulong length, String name) {
            Name = name;
            m_stream = stream;
            m_location = location;
            m_length = length;
        }

        public void Clicked() {
            CaseForm.Instance.HighlightBytes(m_stream, m_location, m_length);
        }

        public override byte GetByte(ulong offset) {
            return m_stream.GetByte(offset);
        }

        public override byte[] GetBytes(ulong offset, ulong length) {
            return m_stream.GetBytes(offset, length);
        }

        public override ulong DeviceOffset {
            get { return m_stream.DeviceOffset; }
        }

        public override ulong StreamLength {
            get { return m_stream.StreamLength; }
        }

        public override string StreamName {
            get { return m_stream.StreamName; }
        }

        public override IDataStream ParentStream {
            get { return m_stream.ParentStream; }
        }

        public override void Open() {
            m_stream.Open();
        }

        public override void Close() {
            m_stream.Close();
        }

        public override long Identifier {
            get { return (long)m_location; }
        }

        public override DateTime LastModified {
            get { throw new NotImplementedException(); }
        }
    }
}
