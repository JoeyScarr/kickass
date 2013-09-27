using System;
using System.Collections.Generic;
using System.Linq;
using FileSystems.FileSystem;

namespace KFA.ApplicationLevel.Registry
{
    public class Registry {
        private const string USER_HIVE = "NTUSER.DAT";
        private const string USER_CLASS_HIVE = "USRCLASS.DAT";
        private const string HKEY_CLASSES_ROOT = "HKEY_CLASSES_ROOT";
        private const string HKEY_CURRENT_USER = "HKEY_CURRENT_USER";
        private const string HKEY_LOCAL_MACHINE = "HKEY_LOCAL_MACHINE";
        private const string HKEY_USERS = "HKEY_USERS";
        private const string HKEY_CURRENT_CONFIG = "HKEY_CURRENT_CONFIG";

        private FileSystem m_fileSystem;
        private Dictionary<string, RegistryHive> hives;

        public Registry(FileSystem fileSystem) {
            m_fileSystem = fileSystem;
            LoadHives();
        }

        public List<VirtualRegistryKey> GetRawHives() {
            List<VirtualRegistryKey> results = new List<VirtualRegistryKey>();
            foreach (var kvp in hives) {
                results.Add(new VirtualRegistryKey(kvp.Value.Root, kvp.Key));
            }
            return results;
        }

        public List<VirtualRegistryKey> GetForUser(String user) {
            List<VirtualRegistryKey> results = new List<VirtualRegistryKey>();
            results.Add(GetKey(HKEY_CLASSES_ROOT, user));
            results.Add(GetKey(HKEY_CURRENT_USER, user));
            results.Add(GetKey(HKEY_LOCAL_MACHINE, user));
            results.Add(GetKey(HKEY_USERS, user));
            return results;
        }

        public List<String> GetUsers() {
            List<String> results = new List<string>();
            foreach (String hiveName in hives.Keys) {
                if (hiveName.Contains("_" + USER_HIVE)) {
                    results.Add(hiveName.Split('_')[0]);
                }
            }
            return results;
        }

        public VirtualRegistryKey GetKey(String path, String user) {
            path = path.Replace('\\', '/');
            String first = path.Split('/')[0];
            String rest = path.Substring(first.Length).TrimStart('/');
            VirtualRegistryKey baseKey = null;
            if(first.Equals(HKEY_LOCAL_MACHINE)) {
                baseKey = new VirtualRegistryKey(HKEY_LOCAL_MACHINE);
                foreach (var kvp in hives) {
                    if (kvp.Value.Path.ToLower().Contains("windows/system32") && !kvp.Value.Path.ToLower().Contains("default")) {
                        VirtualRegistryKey vk = new VirtualRegistryKey(kvp.Key);
                        vk.AddKey(new VirtualRegistryKey(kvp.Value.Root));
                        baseKey.AddChildKey(vk);
                    }
                }
            } else if (first.Equals(HKEY_CURRENT_USER)) {
                baseKey = new VirtualRegistryKey(HKEY_CURRENT_USER);
                RegistryKey userKey = null, classesKey = null;

                foreach (var kvp in hives) {
                    if (kvp.Key == String.Format("{0}_{1}", user, USER_HIVE)) {
                        userKey = kvp.Value.Root;
                    }
                    if (kvp.Key == String.Format("{0}_{1}", user, USER_CLASS_HIVE)) {
                        classesKey = kvp.Value.Root;
                    }
                }

                baseKey.AddKey(new VirtualRegistryKey(userKey));
                VirtualRegistryKey classes = new VirtualRegistryKey("Classes");
                classes.AddKey(new VirtualRegistryKey(classesKey));
                GetKey(baseKey, "Software").AddChildKey(classes);

            } else if (first.Equals(HKEY_CLASSES_ROOT)) {
                baseKey = new VirtualRegistryKey(HKEY_CLASSES_ROOT);
                var k = GetKey(HKEY_CURRENT_USER + "/Software/Classes", user);
                if (k != null) {
                    baseKey.AddKey(k);
                }
                k = GetKey(HKEY_LOCAL_MACHINE + "/Software/Classes", user);
                if (k != null) {
                    baseKey.AddKey(k);
                }
            } else if (first.Equals(HKEY_USERS)) {
                baseKey = new VirtualRegistryKey(HKEY_USERS);
                foreach (var kvp in hives) {
                    if (kvp.Key.Contains("_")) {
                        baseKey.AddChildKey(new VirtualRegistryKey(kvp.Value.Root));
                    }
                }
            }
            return baseKey == null ? null : GetKey(baseKey, rest);
        }

        private VirtualRegistryKey GetKey(VirtualRegistryKey node, String path) {
            if (path.Length == 0) {
                return node;
            }
            String first = path.Split('/')[0];
            String rest = path.Substring(first.Length).TrimStart('/');
            foreach (VirtualRegistryKey child in node.GetChildren()) {
                if (child.Name.ToLower().Equals(first.ToLower())) {
                    return GetKey(child, rest);
                }
            }
            return null;
        }

        private void LoadHives() {
            hives = new Dictionary<String, RegistryHive>();
            List<String> files = new List<string> { "SAM", "SECURITY", "SOFTWARE", "SYSTEM" };
            foreach (var kid in m_fileSystem.GetFile("Windows/System32/config/*")) {
                if (kid as File != null && !System.IO.Path.HasExtension(kid.Name)) {
                try {
                    RegistryHive hive = new RegistryHive((File) kid);
                    String key = (kid as File).Name;
                    hives[key] = hive;
                } catch (Exception) { }
                }
            }
            List<FileSystemNode> users = new List<FileSystemNode>();
            users.AddRange(m_fileSystem.GetFile("Documents and Settings/*"));
            users.AddRange(m_fileSystem.GetFile("Users/*"));
            foreach (var user in users) {
                String[] userHives = { USER_HIVE, 
                                       "Local Settings/Application Data/Microsoft/Windows/" + USER_CLASS_HIVE,
                                       "AppData/Local/Microsoft/Windows/" + USER_CLASS_HIVE
                                     };
                foreach (var userHive in userHives) {
                    String path = (user.Path + userHive).Replace("\\", "");
                    var results = m_fileSystem.GetFile(path);
                    FileSystemNode hiveFile = results.Count() > 0 ? results.First() : null;
                    if (hiveFile != null) {
                        try {
                            RegistryHive hive = new RegistryHive(hiveFile as File);
                            hives.Add(String.Format("{0}_{1}", user.Name, hiveFile.Name.ToUpper()), hive);
                        } catch (Exception) { }
                    }
                }
            }
        }
    }
}