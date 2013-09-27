using System;
using KFS.DataStream;
using KFS.FileSystems;

namespace KFA.ApplicationLevel.Registry {
    public class RegistryHive {
        private RegistryKey m_root;
        private IDataStream m_stream;

        public RegistryHive(File file) {
            m_stream = file;
            try {
                m_root = new RegistryKey(m_stream);
            } catch {
                throw new Exception("Not a valid hive");
            }
        }

        public String Path {
            get {
                return (m_stream as File).Path;
            }
        }

        public RegistryKey Root {
            get {
                return m_root;
            }
        }
    }
}
