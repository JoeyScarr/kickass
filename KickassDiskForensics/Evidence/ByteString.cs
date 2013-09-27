using System;
using KFA.Evidence;

namespace KFS.DataStream {
    class ByteString : IDigitalEvidence {

        public string Name { get; set; }

        public byte[] Bytes { get; set; }

        public ByteString(byte[] bytes) {
            Bytes = bytes;
        }

        #region IDataStream Members

        public byte GetByte(ulong offset) {
            return Bytes[offset];
        }

        public byte[] GetBytes(ulong offset, ulong length) {
            byte[] res = new byte[length];
            Array.Copy(Bytes, (int)offset, res, 0, (int)length);
            return res;
        }

        public ulong StreamLength {
            get { return (ulong) Bytes.Length; }
        }

        public String StreamName {
            get { return "Byte String"; }
        }

        public IDataStream ParentStream {
            get { return null; }
        }

        public ulong DeviceOffset {
            get { return 0; }
        }

        public void Open() {}

        public void Close() {}

        #endregion
    }
}
