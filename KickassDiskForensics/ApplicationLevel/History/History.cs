using System.Collections.Generic;
using KFS.FileSystems;

namespace KFA.ApplicationLevel.History {
    public static class History {
        private static string[] paths = {
            // 2000 / XP
            @"Documents and Settings\*\Local Settings\Temporary Internet Files\Content.ie5",
            @"Documents and Settings\*\Cookies",
            @"Documents and Settings\*\Local Settings\History\history.ie5",
            @"Documents and Settings\*\Local Settings\History\history.ie5\*",
            @"Documents and Settings\*\UserData",
            // Vista / 7
            @"Users\*\AppData\Roaming\Microsoft\Windows\Cookies",
            @"Users\*\AppData\Roaming\Microsoft\Windows\Cookies\Low",
            @"Users\*\AppDataLocal\Microsoft\Windows\History\History.IE5",
            @"Users\*\AppDataLocal\Microsoft\Windows\History\History.IE5\Low",
            @"Users\*\AppData\Local\Microsoft\Windows\History\History.IE5\*",
            @"Users\*\AppData\Local\Microsoft\Windows\History\History.IE5\Low\*",
            @"Users\*\AppData\Local\Microsoft\Windows\Temporary Internet Files\Content.IE5",
            @"Users\*\AppData\Local\Microsoft\Windows\Temporary Internet Files\Low\Content.IE5",
            @"Users\*\AppData\Roaming\Microsoft\Internet Explorer\UserData",
            @"Users\*\AppData\Roaming\Microsoft\Internet Explorer\UserData\Low"
        };
        private static ExplorerHistoryType[] types = {  
            // 2000 / XP
            ExplorerHistoryType.Cache, 
            ExplorerHistoryType.Cookie, 
            ExplorerHistoryType.MainHistory, 
            ExplorerHistoryType.DailyWeeklyHistory,
            ExplorerHistoryType.MainHistory,
            // Vista / 7
            ExplorerHistoryType.Cookie, 
            ExplorerHistoryType.Cookie, 
            ExplorerHistoryType.MainHistory, 
            ExplorerHistoryType.MainHistory, 
            ExplorerHistoryType.DailyWeeklyHistory,
            ExplorerHistoryType.DailyWeeklyHistory,
            ExplorerHistoryType.Cache, 
            ExplorerHistoryType.Cache, 
            ExplorerHistoryType.MainHistory, 
            ExplorerHistoryType.MainHistory
        };
        public static int NumExplorerHistoryFiles {
            get { return paths.Length; }
        }
        public static IEnumerable<ExplorerHistoryFile> GetExplorerHistoryFiles(IFileSystem fileSystem) {
            for (int i = 0; i < paths.Length; i++) {
                foreach (FileSystemNode parent in fileSystem.GetFile(paths[i])) {
                    Folder folder = parent as Folder;
                    if (folder != null) {
                        foreach (FileSystemNode node in folder.GetChildren("index.dat")) {
                            File file = node as File;
                            if (file != null) {
                                yield return new ExplorerHistoryFile(file, folder, types[i]);
                            }
                        }
                    }
                }
            }
        }
    }
}
