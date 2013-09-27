using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KFA.ApplicationLevel.History;
using FileSystems.FileSystem;
using KFA.DataStream;
using KFA.Disks;

namespace KFA.GUI.Explorers {
    public partial class HistoryExplorer : UserControl, IExplorer {
        private IDataStream m_CurrentStream = null;

        public HistoryExplorer() {
            InitializeComponent();
        }

        #region IExplorer Members

        public bool CanView(IDataStream stream) {
            return stream is IFileSystemStore;
        }

        FileSystem fileSystem;

        public void View(IDataStream stream) {
            if (stream != m_CurrentStream) {
                m_CurrentStream = stream;
                lvHistory.Items.Clear();
                if (stream is IFileSystemStore) {
                    fileSystem = (stream as IFileSystemStore).FS;
                    if (fileSystem != null) {
                        // set up Explorer
                        ListViewItem explorerGroup = new ListViewItem(new string[] { "Windows Explorer/IE", "", "" });
                        explorerGroup.Font = new Font(explorerGroup.Font, FontStyle.Bold);
                        lvHistory.Items.Add(explorerGroup);

                        foreach (ExplorerHistoryFile file in History.GetExplorerHistoryFiles(fileSystem)) {
                            ListViewItem fileItem = new ListViewItem(new string[] { file.Path, "", "" });
                            fileItem.Tag = file.Stream;
                            lvHistory.Items.Add(fileItem);
                            foreach (ExplorerHistoryRecord record in file.GetRecords()) {
                                ListViewItem recordItem = new ListViewItem(
                                    new string[] {  record.Description,
                                            record.LastAccessed.ToString(),
                                            record.LastModified.ToString() });
                                recordItem.Tag = record;
                                lvHistory.Items.Add(recordItem);
                            }
                        }

                        // set up Firefox (TODO in future)
                        //ListViewItem firefoxGroup = new ListViewItem(new string[] { "Firefox", "", "" });
                        //firefoxGroup.Font = new Font(firefoxGroup.Font, FontStyle.Bold);
                        //lvHistory.Items.Add(firefoxGroup);
                    }
                }
            }
        }

        

        #endregion

        private void lvHistory_SelectedIndexChanged(object sender, EventArgs e) {
            // If there's a cached item for this record, we can view it
            if (lvHistory.SelectedItems.Count > 0) {
                IDataStream stream = lvHistory.SelectedItems[0].Tag as IDataStream;
                if (stream != null) {
                    CaseForm.Instance.ViewableObject = stream;
                } else {
                    ExplorerHistoryRecord record = lvHistory.SelectedItems[0].Tag as ExplorerHistoryRecord;
                    if (record != null && record.DataStream != null) {
                        CaseForm.Instance.ViewableObject = record.DataStream;
                    }
                }
            }
        }
    }
}
