using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using KFA.DataStream;
using KFA.Disks;
using KFA.Evidence;
using KFA.GUI;
using KFA.GUI.Explorers;
using KFA.GUI.Viewers;
using KFA.Search;
using Microsoft.Win32;
using System.Management;
using System.Threading;
using System.IO;
using Image = KFA.Disks.Image;
using FileSystems;
using FileSystems.FileSystem;
using File = FileSystems.FileSystem.File;

namespace KFA {
    public partial class CaseForm : Form {

        private static CaseForm inst;
        private IDataStream m_viewableObject;
        private IDataStream m_explorableObject;

        public Case ActiveCase { get; private set; }

        TreeNode m_CaseRoot;
        TreeNode m_DiskRoot;
        private List<TreeNode> m_disks;
        TreeNode m_LogicalDiskRoot;
        private List<TreeNode> m_logicalDisks;

        public CaseForm() {
            InitializeComponent();
            inst = this;

            // Set up the case tree
            caseTreeView.ImageList = imageList1;
            m_CaseRoot = new TreeNode("Case");
            m_CaseRoot.ImageKey = "MagGlass";
            m_CaseRoot.SelectedImageKey = "MagGlass";
            caseTreeView.Nodes.Add(m_CaseRoot);

            // Set up the disk trees
            m_DiskRoot = new TreeNode("Physical Disks");
            rightClickActions[m_DiskRoot] = RefreshDisks;
            m_DiskRoot.ImageKey = "HDD";
            m_DiskRoot.SelectedImageKey = "HDD";
            caseTreeView.Nodes.Add(m_DiskRoot);
            m_LogicalDiskRoot = new TreeNode("Logical Disks");
            rightClickActions[m_LogicalDiskRoot] = RefreshLogicalDisks;
            m_LogicalDiskRoot.ImageKey = "HDD";
            m_LogicalDiskRoot.SelectedImageKey = "HDD";
            caseTreeView.Nodes.Add(m_LogicalDiskRoot);

            List<KeyValuePair<String, Action>> actions = new List<KeyValuePair<string, Action>>();
            actions.Add(new KeyValuePair<string, Action>("Initialising", delegate() {
                Thread.Sleep(500);
            }));
            actions.Add(new KeyValuePair<string, Action>("Loading Physical Disks", delegate() {
                LoadDisks();
            }));
            actions.Add(new KeyValuePair<string, Action>("Loading Logical Disks", delegate() {
                LoadLogicalVolumes();
            }));
            actions.Add(new KeyValuePair<string, Action>("Loading Case", delegate() {
                LoadDefaultCase();
            }));

            SplashScreen form = new SplashScreen(actions);
            form.ShowDialog();

            UpdateDiskTree();
            SetCaption(ActiveCase.Name);
            RefreshCaseTree();
        }

        public static CaseForm Instance {
            get {
                return inst;
            }
        }

        public IDataStream ViewableObject {
            get {
                return m_viewableObject;
            }
            set {
                this.m_viewableObject = value;
                if (value is TextSearchResult) {
                } else if (value is File) {
                    ActiveCase.LogAction("Viewed file " + (value as File).Name, ActionType.FileViewed);
                } else {
                    ActiveCase.LogAction("Viewed " + value, ActionType.StreamViewed);
                }
                lViewing.Text = "Currently viewing: " + value.ToString();
                UpdateViewers();
            }
        }

        public IDataStream ExplorableObject {
            get {
                return m_explorableObject;
            }
            set {
                this.m_explorableObject = value;
                ActiveCase.LogAction("Viewed " + value, ActionType.StreamExplored);
                UpdateExplorer(tabExplorers.SelectedTab);
            }
        }

        public void AddSearchResult(SearchResults searchResults) {
            SearchResultExplorer explorer = (SearchResultExplorer)tabPage5.Controls[0];
            explorer.AddResults(searchResults);
            tabExplorers.SelectedTab = tabPage5;
        }

        public void HighlightBytes(IDataStream stream, ulong start, ulong length) {
            ViewableObject = stream;
            hexControl1.Select(start, start + length);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            ActiveCase.LogAction("Session ended", ActionType.SessionEnded);
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            About aboutForm = new About();
            aboutForm.ShowDialog();
        }

        private void markAsEvidenceToolStripMenuItem_Click(object sender, EventArgs e) {
            saveHexSelectionAsEvidence();
        }

        private void saveHexSelectionAsEvidence() {
            /*HexControl active = null;
            foreach (Form f in mdiClient.Controls) {
                if (!f.ContainsFocus) {
                    continue;
                }
                foreach (Control c in f.Controls) {
                    if (c is HexControl) {
                        active = (HexControl)c;
                        break;
                    }
                }
            }

            if (active != null) {
                byte[] bytes = active.GetSelectedBytes();
                if (bytes != null && bytes.Length > 0) {
                    ActiveCase.LogAction("Copying string of bytes to evidence!");
                    IDigitalEvidence evidence = new ByteString(bytes);
                    //ActiveCase.
                }
            }*/
        }

        private void LoadDefaultCase() {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dir = Path.Combine(dir, "KFA Forensics");
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
            String defaultCasePath = Path.Combine(dir, "default.case");
            if (!System.IO.File.Exists(defaultCasePath)) {
                ActiveCase = new Case(defaultCasePath, "Default Case");
                ActiveCase.LogAction(String.Format("Default Case Created @ {0}", defaultCasePath), ActionType.CaseCreated);
            }
            LoadCase(defaultCasePath, false);
        }

        private bool NewCase() {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Case Files|*.case";
            saveFileDialog.Title = "New Case Location";

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                Case newCase = new Case(saveFileDialog.FileName, Path.GetFileNameWithoutExtension(saveFileDialog.FileName));
                newCase.LogAction(String.Format("Case '{0} created and saved to {1}", newCase.Name, saveFileDialog.FileName), ActionType.CaseCreated);
                newCase.Save();

                LoadCase(saveFileDialog.FileName, true);
                Form form = new CaseInfo(ActiveCase);
                form.ShowDialog();
                return true;
            }
            return false;
        }

        private bool LoadCase(String filePath, bool remember) {
            Case newCase;
            try {
                newCase = Case.Deserialize(filePath);
            } catch (Exception) {
                MessageBox.Show("Failed to load case!");
                return false;
            }
            if (remember) {
                List<String> bits = new List<string>();
                RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\KFA");
                if (key != null) {
                    String[] exBits = (key.GetValue("LastCase") as String).Split('|');
                    bits.AddRange(exBits);
                    if (bits.Contains(filePath)) {
                        bits.Remove(filePath);
                    }
                    bits.Insert(0, filePath);
                }
                key.Close();
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\KFA", true);
                String n = String.Join("|", bits.ToArray());
                key.SetValue("LastCase", n);
            }

            ActiveCase = newCase;
            SetCaption(ActiveCase.Name);
            RefreshCaseTree();
            UpdateRecent();
            ActiveCase.LogAction("Session started", ActionType.SessionStarted);

            return true;
        }

        private void UpdateRecent() {
            recentCasesToolStripMenuItem.DropDownItems.Clear();
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\KFA");
            if (key != null) {
                String vals = key.GetValue("LastCase") as String;
                foreach (String recent in vals.Split('|')) {
                    ToolStripItem item = new ToolStripMenuItem(Path.GetFileNameWithoutExtension(recent));
                    item.Tag = recent;
                    item.Click += delegate(object sender, EventArgs e) {
                        LoadCase(((ToolStripItem)sender).Tag as String, true);
                    };
                    recentCasesToolStripMenuItem.DropDownItems.Add(item);
                }

            }
            recentCasesToolStripMenuItem.Enabled = recentCasesToolStripMenuItem.DropDownItems.Count > 0;
        }

        private void loadCaseToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Case Files|*.case";
            openFileDialog.Title = "Select a File";

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                LoadCase(openFileDialog.FileName, true);
            }
        }

        private void LoadLastCase() {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\KFA");
            if (key != null) {
                String last = key.GetValue("LastCase") as String;
                if (!LoadCase(last, true)) {
                    if (!NewCase()) {
                        Application.Exit();
                    }
                }
            } else {
                if (!NewCase()) {
                    Application.Exit();
                }
            }
        }

        private void SetCaption(String text) {
            this.Text = String.Format("KFA Forensics Suite [{0}]", text);
        }

        public List<IDataStream> GetStreamHeirachy() {
            List<IDataStream> result = new List<IDataStream>();
            IDataStream stream = m_viewableObject;
            while (stream != null) {
                result.Add(stream);
                stream = stream.ParentStream;
            }

            return result;
        }

        private void newCaseToolStripMenuItem_Click(object sender, EventArgs e) {
            NewCase();
        }

        public void UpdateViewers() {
            foreach (TabPage tp in tabViewers.TabPages) {
                if (tp.Controls.Count > 0) {
                    IDataViewer viewer = (IDataViewer)tp.Controls[0];
                    tp.Enabled = viewer.CanView(ViewableObject);
                    if (tp.Enabled) {
                        viewer.View(ViewableObject);//, m_start, m_end);
                    }
                } else {
                    tp.Enabled = false;
                }
            }
            searchToolStripMenuItem1.Enabled = ViewableObject != null;
        }

        public void UpdateExplorer(TabPage tp) {
            if (tp.Controls.Count > 0) {
                IExplorer explorer = (IExplorer)tp.Controls[0];
                tp.Enabled = explorer.CanView(ExplorableObject);
                if (tp.Enabled) {
                    explorer.View(ExplorableObject);//, m_start, m_end);
                }
            } else {
                tp.Enabled = false;
            }
        }

        private void SaveDiskImage(TreeNodeMouseClickEventArgs e) {
            IImageable disk = e.Node.Tag as IImageable;
            if (disk != null) {
                ContextMenu menu = new ContextMenu();
                menu.MenuItems.Add(new MenuItem("Save Image", new EventHandler(delegate(object o, EventArgs ea) {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Any Files|*.*";
                    saveFileDialog.Title = "Select a Location";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                        ImageForm imageForm = new ImageForm();
                        imageForm.Show();
                        imageForm.CreateImage(disk, saveFileDialog.FileName);
                    }
                })));
                menu.Show(e.Node.TreeView, e.Location);
            }
        }

        private void RefreshDisks(TreeNodeMouseClickEventArgs e) {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Refresh", new EventHandler(delegate(object o, EventArgs ea) {
                LoadDisks();
                UpdateDiskTree();
            })));
            menu.Show(e.Node.TreeView, e.Location);
        }

        private void RefreshLogicalDisks(TreeNodeMouseClickEventArgs e) {
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Refresh", new EventHandler(delegate(object o, EventArgs ea) {
                LoadLogicalVolumes();
                UpdateDiskTree();
            })));
            menu.Show(e.Node.TreeView, e.Location);
        }

        private void UpdateDiskTree() {
            foreach (TreeNode node in m_DiskRoot.Nodes) {
                rightClickActions.Remove(node);
            }
            foreach (TreeNode node in m_LogicalDiskRoot.Nodes) {
                rightClickActions.Remove(node);
            }
            m_DiskRoot.Nodes.Clear();
            m_LogicalDiskRoot.Nodes.Clear();

            foreach (var node in m_disks) {
                m_DiskRoot.Nodes.Add(node);
            }
            foreach (var node in m_logicalDisks) {
                m_LogicalDiskRoot.Nodes.Add(node);
            }

            m_DiskRoot.ExpandAll();
            m_LogicalDiskRoot.ExpandAll();
        }

        private void LoadDisks() {
            m_disks = new List<TreeNode>();
            foreach (PhysicalDisk disk in DiskLoader.LoadDisks()) {
                TreeNode node = new TreeNode(disk.ToString());
                node.Tag = disk;
                rightClickActions[node] = SaveDiskImage;
                m_disks.Add(node);
                foreach (PhysicalDiskSection section in disk.Sections) {
                    TreeNode sectionNode = new TreeNode(section.ToString());
                    sectionNode.Tag = section;
                    rightClickActions[sectionNode] = SaveDiskImage;
                    node.Nodes.Add(sectionNode);
                }
            }
        }

        private void LoadLogicalVolumes() {
            m_logicalDisks = new List<TreeNode>();
            foreach (LogicalDisk disk in DiskLoader.LoadLogicalVolumes()) {
                TreeNode node = new TreeNode(disk.ToString());
                node.Tag = disk;
                m_logicalDisks.Add(node);
            }
        }

        public void RefreshCaseTree() {
            m_CaseRoot.Nodes.Clear();

            //Images
            TreeNode images = new TreeNode("Images");
            images.ImageKey = "Images";
            images.SelectedImageKey = "Images";
            foreach (Disks.Image image in ActiveCase.Images) {
                TreeNode imageNode = new TreeNode(image.ToString());
                imageNode.Tag = image;
                imageNode.ImageKey = "Images";
                imageNode.SelectedImageKey = "Images";
                doubleClickActions[imageNode] = delegate(TreeNodeMouseClickEventArgs e) {
                    ExplorableObject = ViewableObject = (IDataStream)e.Node.Tag;
                };
                images.Nodes.Add(imageNode);
            }

            TreeNode caseSettings = new TreeNode("Case Info");
            caseSettings.ImageKey = "Logs";
            caseSettings.SelectedImageKey = "Logs";
            doubleClickActions[caseSettings] = delegate(TreeNodeMouseClickEventArgs node) {
                Form caseForm = new CaseInfo(ActiveCase);
                caseForm.ShowDialog();
            };

            TreeNode actionLog = new TreeNode("Action Log");
            actionLog.ImageKey = "Logs";
            actionLog.SelectedImageKey = "Logs";
            doubleClickActions[actionLog] = delegate(TreeNodeMouseClickEventArgs node) {
                Form logForm = new LogForm(ActiveCase.Actions);
                logForm.Show();
            };

            TreeNode digital = new TreeNode("Evidence");
            digital.ImageKey = "Thumbprint";
            digital.SelectedImageKey = "Thumbprint";

            m_CaseRoot.Nodes.Add(caseSettings);
            m_CaseRoot.Nodes.Add(actionLog);
            m_CaseRoot.Nodes.Add(images);
            m_CaseRoot.Nodes.Add(digital);

            m_CaseRoot.ExpandAll();
        }

        private Dictionary<TreeNode, Action<TreeNodeMouseClickEventArgs>> doubleClickActions = new Dictionary<TreeNode, Action<TreeNodeMouseClickEventArgs>>();
        private Dictionary<TreeNode, Action<TreeNodeMouseClickEventArgs>> rightClickActions = new Dictionary<TreeNode, Action<TreeNodeMouseClickEventArgs>>();

        private void caseTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (doubleClickActions.ContainsKey(e.Node)) {
                doubleClickActions[e.Node](e);
            }
        }

        private void showLogToolStripMenuItem_Click(object sender, EventArgs e) {
            Form logForm = new LogForm(ActiveCase.Actions);
            logForm.Show();
        }

        private void caseTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node.Tag is IDataStream) {
                // show it
                ExplorableObject = ViewableObject = (IDataStream)e.Node.Tag;
            }
        }

        private void caseTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (rightClickActions.ContainsKey(e.Node)) {
                    rightClickActions[e.Node](e);
                }
            }
        }

        private void textHexSearchToolStripMenuItem_Click(object sender, EventArgs e) {
            Form searchForm = new TextSearchForm(m_viewableObject);
            searchForm.Show();
        }

        private void fileSearchToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!(ExplorableObject is IFileSystemStore)) {
                return;
            }
            IFileSystemStore store = ExplorableObject as IFileSystemStore;
            if (store.FS != null) {
                Form searchForm = new FileSearchForm(store.FS);
                searchForm.Show();
            }
        }

        private void tabExplorers_SelectedIndexChanged(object sender, EventArgs e) {
            ActiveCase.LogAction("Using explorer '" + tabExplorers.SelectedTab.Text + "'", ActionType.StreamExplored);
            UpdateExplorer(tabExplorers.SelectedTab);
        }

        private void tabViewers_SelectedIndexChanged(object sender, EventArgs e) {
            ActiveCase.LogAction("Using viewer '" + tabViewers.SelectedTab.Text + "'", ActionType.StreamViewed);
        }

        private void caseInfoToolStripMenuItem_Click(object sender, EventArgs e) {
            Form form = new CaseInfo(ActiveCase);
            form.ShowDialog();
        }

        private void userDocsToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start(String.Format(@"{0}\UserGuide.pdf", Path.GetDirectoryName(Application.ExecutablePath)));
        }

        private void fileExplorer1_StreamSelected(object sender, IDataStream stream) {
            ViewableObject = stream;
        }
    }
}
