using System;
using System.Collections.Generic;
using System.Linq;
using KFS.DataStream;

namespace KFA.ApplicationLevel.Registry {

    public struct KeyBlock {
        public UInt32 blockSize;
        public UInt32 subkeyCount;
        public UInt32 subkeys;
        public UInt32 valueCount;
        public Int32 offsets;
        public short len, du;
        public String name, root;
    }

    public class RegistryKey {
        private IDataStream m_Stream;
        private KeyBlock m_keyBlock;
        private ulong m_blockOffset;

        public static ulong ROOT_LENGTH = 0x20, ROOT = 0x1000;

        public enum ValueType {
            REG_NONE = 0,
            REG_SZ,
            REG_EXPAND_SZ,
            REG_BINARY,
            REG_DWORD,
            REG_DWORD_BIG_ENDIAN,
            REG_LINK,
            REG_MULTI_SZ,
            REG_RESOURCE_LIST,
            REG_FULL_RESOURCE_DESCRIPTOR,
            REG_RESOURCE_REQUIREMENTS_LIST,
            REG_QWORD
        }

        public RegistryKey(IDataStream stream, ulong offset) {
            m_Stream = stream;
            m_blockOffset = offset;
            m_keyBlock = LoadBlock(stream, offset);
            Name = m_keyBlock.name;
        }

        public RegistryKey(IDataStream stream)
            : this(stream, ROOT + 0x20) { }

        public RegistryKey(IDataStream stream, ulong offset, String name)
            : this(stream, offset) {
            Name = name;
        }

        public bool HasKids {
            get { return m_keyBlock.subkeyCount > 0; }
        }

        public String Name {
            get;
            private set;
        }

        public List<RegistryKey> GetChildren() {
            List<RegistryKey> children = new List<RegistryKey>();
            foreach (UInt32 offset in LoadOffsets(m_Stream, ROOT + m_keyBlock.subkeys)) {
                children.Add(new RegistryKey(m_Stream, offset));
            }
            return children;
        }

        public Dictionary<String, String> GetValues() {
            Dictionary<String, String> result = new Dictionary<string, string>();
            for (int i = 0; i < m_keyBlock.valueCount; i++) {
                ulong vOffset = Util.GetUInt32(m_Stream, (UInt32)m_keyBlock.offsets + ROOT + 4ul + ((ulong)i * 4ul)) + ROOT;
                String blockType = Util.GetASCIIString(m_Stream, vOffset, 4);
                UInt16 nameLen = Util.GetUInt16(m_Stream, vOffset + 6);
                UInt32 size = Util.GetUInt32(m_Stream, vOffset + 8);
                UInt32 off = Util.GetUInt32(m_Stream, vOffset + 12);
                ValueType type = (ValueType)Util.GetUInt32(m_Stream, vOffset + 16);
                String name = Util.GetASCIIString(m_Stream, vOffset + 24, nameLen);
                if (name == "") {
                    name = "Default";
                }

                if (vOffset == 0) {
                    throw new Exception("Bad registry value");
                }

                ulong data = ROOT + off + 4;
                if ((size & 1 << 31) > 0) {
                    data = vOffset + 12;
                }

                uint lenBytes = size & 0xffff;
                String v = "";
                if (type == ValueType.REG_SZ || type == ValueType.REG_EXPAND_SZ) {
                    v += Util.GetUnicodeString(m_Stream, data, (ulong)lenBytes);
                } else if (type == ValueType.REG_DWORD) {
                    v += Util.GetUInt32(m_Stream, data);
                } else if (type == ValueType.REG_LINK) {
                    v += "REG_LINK";
                } else {
                    v += String.Format("{0}", Util.GetHexString(m_Stream, data, lenBytes));
                }

                result[name] = v;
            }
            return result;
        }

        private static KeyBlock LoadBlock(IDataStream stream, ulong blockOffset) {
            ulong offset = blockOffset;

            KeyBlock result = new KeyBlock();
            result.blockSize = Util.GetUInt32(stream, offset);
            result.subkeyCount = Util.GetUInt32(stream, offset + 24);
            result.subkeys = Util.GetUInt32(stream, offset + 32);
            result.valueCount = Util.GetUInt32(stream, offset + 40);
            result.offsets = Util.GetInt32(stream, offset + 44);
            result.len = Util.GetInt16(stream, offset + 76);
            result.du = Util.GetInt16(stream, offset + 78);

            result.name = Util.GetASCIIString(stream, offset + 80, (ulong)result.len);
            return result;
        }

        private List<UInt32> LoadOffsets(IDataStream stream, ulong startOffset) {
            if (m_keyBlock.subkeyCount == 0) {
                return new List<uint>();
            }

            ulong offset = startOffset;
            String blockType = Util.GetASCIIString(stream, offset + 4, 2);
            ulong count = Util.GetUInt16(stream, offset + 6);

            offset += 8ul;
            List<UInt32> result = new List<uint>();
            for (ulong i = 0; i < count; i++) {
                if (blockType[1] == 'f' || blockType[1] == 'h') {
                    result.Add(Util.GetUInt32(stream, (uint)(offset + (8ul * i))) + (UInt32)ROOT);
                } else {
                    UInt32 subOffset = Util.GetUInt32(stream, (uint)(offset + (4ul * i))) + (UInt32)ROOT;
                    List<UInt32> subItems = LoadOffsets(m_Stream, subOffset);
                    subOffset += 8;
                    for (int j = 0; j < subItems.Count; j++) {
                        ulong s = blockType[1] == 'i' ? 8ul : 4ul;
                        result.Add(Util.GetUInt32(stream, (uint)(subOffset + (8ul * (ulong)j))) + (UInt32)ROOT);
                    }
                }
            }

            return result;
        }
    }


    public class VirtualRegistryKey {

        private List<RegistryKey> m_keys;
        private List<VirtualRegistryKey> m_virtualChildren;
        private List<VirtualRegistryKey> children;
        private String m_name;

        public VirtualRegistryKey(String name) {
            m_name = name;
            m_keys = new List<RegistryKey>();
            m_virtualChildren = new List<VirtualRegistryKey>();
        }

        public VirtualRegistryKey(RegistryKey realKey, String name)
            : this(realKey) {
            m_name = name;
        }

        public VirtualRegistryKey(RegistryKey realKey) {
            m_keys = new List<RegistryKey> { realKey };
            m_virtualChildren = new List<VirtualRegistryKey>();
        }

        public void AddKey(VirtualRegistryKey key) {
            m_keys.AddRange(key.m_keys);
        }

        public void AddChildKey(VirtualRegistryKey child) {
            m_virtualChildren.Add(child);
        }

        public List<VirtualRegistryKey> GetChildren() {
            if (children == null) {
                Dictionary<String, VirtualRegistryKey> kids = new Dictionary<string, VirtualRegistryKey>();
                foreach (RegistryKey realKey in m_keys) {
                    if (realKey != null) {
                        foreach (RegistryKey kid in realKey.GetChildren()) {
                            if (!kids.ContainsKey(kid.Name)) {
                                kids[kid.Name] = new VirtualRegistryKey(kid);
                            } else {
                                kids[kid.Name].m_keys.Add(kid);
                            }
                        }
                    }
                }
                foreach (VirtualRegistryKey vc in m_virtualChildren) {
                    kids[vc.Name] = vc;
                }
                children = kids.Values.OrderBy(x => x.Name).ToList();
            }

            return children;
        }

        public Dictionary<String, String> GetValues() {
            Dictionary<String, String> values = new Dictionary<string, string>();
            foreach (RegistryKey realKey in m_keys) {
                foreach (var kvp in realKey.GetValues()) {
                    if (realKey != null) {
                        if (!values.ContainsKey(kvp.Key)) {
                            values[kvp.Key] = kvp.Value;
                        }
                    }
                }
            }
            return values;
        }

        public String GetValue(String name) {
            foreach (RegistryKey realKey in m_keys) {
                var dict = realKey.GetValues();
                if (dict.ContainsKey(name)) {
                    return dict[name];
                }
            }
            return null;
        }

        public String Name {
            get {
                return m_name != null ? m_name : m_keys.First().Name;
            }
        }

        public override String ToString() {
            return Name;
        }
    }
}
