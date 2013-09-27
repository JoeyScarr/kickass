using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using KFS.FileSystems;
using KFS.FileSystems.NTFS;
using KFA.Search;

namespace KFA.GUI {
    public partial class FileSearchForm : Form {
        private Dictionary<FileType, HashSet<String>> m_fileTypeFilters;
        private IFileSystem m_fileSystem;
        private String[] fileTypes;
        private Thread finder;
        private FileType selectedType = FileType.All;

        public FileSearchForm(IFileSystem fs) {
            InitializeComponent();

            m_fileSystem = fs;

            m_fileTypeFilters = FileTypes.SupportedFileTypes;
            m_fileTypeFilters.Add(FileType.All, new HashSet<string>());

            foreach (FileType key in m_fileTypeFilters.Keys) {
                comboBoxFileFilters.Items.Add(key);
            }
            comboBoxFileFilters.SelectedIndex = 1;
            comboBoxFileFilters_SelectionChangeCommitted(null, null);
            label1.Text = "";
        }

        private string SetToString(HashSet<string> set) {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (string s in set) {
                if (!first) sb.Append(", ");
                sb.Append(s);
                first = false;
            }
            return sb.ToString();
        }

        private void comboBoxFileFilters_SelectionChangeCommitted(object sender, EventArgs e) {
            if (comboBoxFileFilters.Text == "Custom") {
                textBoxTypeFilter.Enabled = true;
                textBoxTypeFilter.Text = "";
            } else {
                textBoxTypeFilter.Enabled = false;
                textBoxTypeFilter.Text = SetToString(m_fileTypeFilters[(FileType)comboBoxFileFilters.SelectedItem]);
            }
        }

        private void Find() {
            selectedType = (FileType)comboBoxFileFilters.SelectedItem;
            finder = new Thread(delegate() {
                List<IFileSystemNode> results = new List<IFileSystemNode>();
                fileTypes = textBoxTypeFilter.Text.Split(',');
                try {
                    this.Invoke(new Action(delegate() {
                        btnCancel.Enabled = true;
                        button1.Enabled = false;
                    }));
                    m_fileSystem.GetDefaultSearchStrategy().Search(delegate(INodeMetadata metadata, ulong current, ulong total) {
                        this.Invoke(new Action(delegate() {
                            if (current % 1000 == 0) {
                                label1.Text = string.Format("{0:0.0}% {1}", (double)current / (double)total * 100, metadata.Name);
                            }
                        }));
                        if (CheckFile(metadata)) {
                            results.Add(metadata.GetFileSystemNode());
                        }
                        return true;
                    });
                    SearchFinished(results);
                } catch (ThreadAbortException) { }
            });
            finder.Start();
        }

        private bool CheckFile(INodeMetadata metadata) {
            if (cbDeleted.Checked) {
                if (!metadata.Deleted) {
                    return false;
                }
            }

            IFileSystemNode node = metadata.GetFileSystemNode();
            String name = node.Name;

            if (cbAltStreams.Checked) {
                if (!(node is HiddenDataStreamFileNTFS)) {
                    return false;
                }
                name = name.Replace("(Hidden Streams)", "");
            }

            File file = node as File;
            if (file == null) {
                return false;
            }
            return FileTypes.IsFileType(file, selectedType);
        }

        private void SearchFinished(IList<IFileSystemNode> results) {
            MessageBox.Show("Seach complete - found " + results.Count + " results");
            SearchResults sr = new SearchResults("File Search Results", results);

            this.Invoke(new Action(delegate() {
                CaseForm.Instance.AddSearchResult(sr);
                this.Dispose();
            }));
        }

        private void button1_Click(object sender, EventArgs e) {
            Find();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            finder.Abort();
            this.Dispose();
        }
    }
}
