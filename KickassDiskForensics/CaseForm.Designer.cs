using KFA.GUI.Explorers;
using KFA.GUI.Viewers;

namespace KFA {
    partial class CaseForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaseForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentCasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.caseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.textHexSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evidenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsEvidenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userDocsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelCase = new System.Windows.Forms.Panel();
            this.caseTreeView = new System.Windows.Forms.TreeView();
            this.panelExplorers = new System.Windows.Forms.Panel();
            this.tabExplorers = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.diskExplorer1 = new KFA.GUI.Explorers.DiskExplorer();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.sectorExplorerControl1 = new KFA.GUI.Explorers.SectorExplorer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.fileExplorer1 = new KFA.GUI.Explorers.FileExplorer();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.registryExplorer1 = new KFA.GUI.Explorers.RegistryExplorer();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.historyExplorer1 = new KFA.GUI.Explorers.HistoryExplorer();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.timelineExplorer1 = new KFA.GUI.Explorers.TimelineExplorer();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.searchResultExplorer1 = new KFA.GUI.Explorers.SearchResultExplorer();
            this.panelViewers = new System.Windows.Forms.Panel();
            this.tabViewers = new System.Windows.Forms.TabControl();
            this.tabPageHex = new System.Windows.Forms.TabPage();
            this.hexControl1 = new KFA.GUI.Viewers.HexControl();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.textControl1 = new KFA.GUI.Viewers.TextControl();
            this.tabPageImage = new System.Windows.Forms.TabPage();
            this.pictureControl1 = new KFA.GUI.Viewers.PictureControl();
            this.tabPageGallery = new System.Windows.Forms.TabPage();
            this.gallery1 = new KFA.GUI.Viewers.Gallery();
            this.tabPageHtml = new System.Windows.Forms.TabPage();
            this.webBrowserControl1 = new KFA.GUI.Viewers.WebBrowserControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.histogramControl1 = new KFA.GUI.Viewers.HistogramControl();
            this.lViewing = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelCase.SuspendLayout();
            this.panelExplorers.SuspendLayout();
            this.tabExplorers.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panelViewers.SuspendLayout();
            this.tabViewers.SuspendLayout();
            this.tabPageHex.SuspendLayout();
            this.tabPageText.SuspendLayout();
            this.tabPageImage.SuspendLayout();
            this.tabPageGallery.SuspendLayout();
            this.tabPageHtml.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.searchToolStripMenuItem1,
            this.evidenceToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(944, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCaseToolStripMenuItem,
            this.loadCaseToolStripMenuItem,
            this.recentCasesToolStripMenuItem,
            this.toolStripSeparator1,
            this.caseInfoToolStripMenuItem,
            this.showLogToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newCaseToolStripMenuItem
            // 
            this.newCaseToolStripMenuItem.Name = "newCaseToolStripMenuItem";
            this.newCaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newCaseToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.newCaseToolStripMenuItem.Text = "New Case...";
            this.newCaseToolStripMenuItem.Click += new System.EventHandler(this.newCaseToolStripMenuItem_Click);
            // 
            // loadCaseToolStripMenuItem
            // 
            this.loadCaseToolStripMenuItem.Name = "loadCaseToolStripMenuItem";
            this.loadCaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadCaseToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.loadCaseToolStripMenuItem.Text = "Open Case...";
            this.loadCaseToolStripMenuItem.Click += new System.EventHandler(this.loadCaseToolStripMenuItem_Click);
            // 
            // recentCasesToolStripMenuItem
            // 
            this.recentCasesToolStripMenuItem.Name = "recentCasesToolStripMenuItem";
            this.recentCasesToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.recentCasesToolStripMenuItem.Text = "Recent Cases";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // caseInfoToolStripMenuItem
            // 
            this.caseInfoToolStripMenuItem.Name = "caseInfoToolStripMenuItem";
            this.caseInfoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.caseInfoToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.caseInfoToolStripMenuItem.Text = "Case Info...";
            this.caseInfoToolStripMenuItem.Click += new System.EventHandler(this.caseInfoToolStripMenuItem_Click);
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.showLogToolStripMenuItem.Text = "Show Log";
            this.showLogToolStripMenuItem.Click += new System.EventHandler(this.showLogToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem1
            // 
            this.searchToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textHexSearchToolStripMenuItem,
            this.fileSearchToolStripMenuItem});
            this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
            this.searchToolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.searchToolStripMenuItem1.Text = "Search";
            // 
            // textHexSearchToolStripMenuItem
            // 
            this.textHexSearchToolStripMenuItem.Name = "textHexSearchToolStripMenuItem";
            this.textHexSearchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.textHexSearchToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.textHexSearchToolStripMenuItem.Text = "Text/Hex Search";
            this.textHexSearchToolStripMenuItem.Click += new System.EventHandler(this.textHexSearchToolStripMenuItem_Click);
            // 
            // fileSearchToolStripMenuItem
            // 
            this.fileSearchToolStripMenuItem.Name = "fileSearchToolStripMenuItem";
            this.fileSearchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.F)));
            this.fileSearchToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.fileSearchToolStripMenuItem.Text = "File Search";
            this.fileSearchToolStripMenuItem.Click += new System.EventHandler(this.fileSearchToolStripMenuItem_Click);
            // 
            // evidenceToolStripMenuItem
            // 
            this.evidenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAsEvidenceToolStripMenuItem});
            this.evidenceToolStripMenuItem.Name = "evidenceToolStripMenuItem";
            this.evidenceToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.evidenceToolStripMenuItem.Text = "Evidence";
            // 
            // markAsEvidenceToolStripMenuItem
            // 
            this.markAsEvidenceToolStripMenuItem.Name = "markAsEvidenceToolStripMenuItem";
            this.markAsEvidenceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.markAsEvidenceToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.markAsEvidenceToolStripMenuItem.Text = "Mark as Evidence";
            this.markAsEvidenceToolStripMenuItem.Click += new System.EventHandler(this.markAsEvidenceToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userDocsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // userDocsToolStripMenuItem
            // 
            this.userDocsToolStripMenuItem.Name = "userDocsToolStripMenuItem";
            this.userDocsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.userDocsToolStripMenuItem.Text = "User Docs";
            this.userDocsToolStripMenuItem.Click += new System.EventHandler(this.userDocsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.aboutToolStripMenuItem.Text = "About KFA";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 583);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(944, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "HDD");
            this.imageList1.Images.SetKeyName(1, "Thumbprint");
            this.imageList1.Images.SetKeyName(2, "MagGlass");
            this.imageList1.Images.SetKeyName(3, "Logs");
            this.imageList1.Images.SetKeyName(4, "Images");
            this.imageList1.Images.SetKeyName(5, "DeletedFile");
            this.imageList1.Images.SetKeyName(6, "File");
            this.imageList1.Images.SetKeyName(7, "Directory");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelViewers);
            this.splitContainer1.Size = new System.Drawing.Size(944, 559);
            this.splitContainer1.SplitterDistance = 552;
            this.splitContainer1.TabIndex = 9;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panelCase);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panelExplorers);
            this.splitContainer2.Size = new System.Drawing.Size(552, 559);
            this.splitContainer2.SplitterDistance = 154;
            this.splitContainer2.TabIndex = 0;
            // 
            // panelCase
            // 
            this.panelCase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelCase.Controls.Add(this.caseTreeView);
            this.panelCase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCase.Location = new System.Drawing.Point(0, 0);
            this.panelCase.Name = "panelCase";
            this.panelCase.Size = new System.Drawing.Size(154, 559);
            this.panelCase.TabIndex = 5;
            // 
            // caseTreeView
            // 
            this.caseTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.caseTreeView.HideSelection = false;
            this.caseTreeView.Location = new System.Drawing.Point(3, 3);
            this.caseTreeView.Name = "caseTreeView";
            this.caseTreeView.Size = new System.Drawing.Size(144, 549);
            this.caseTreeView.TabIndex = 0;
            this.caseTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.caseTreeView_NodeMouseDoubleClick);
            this.caseTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.caseTreeView_AfterSelect);
            this.caseTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.caseTreeView_NodeMouseClick);
            // 
            // panelExplorers
            // 
            this.panelExplorers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelExplorers.Controls.Add(this.tabExplorers);
            this.panelExplorers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExplorers.Location = new System.Drawing.Point(0, 0);
            this.panelExplorers.Name = "panelExplorers";
            this.panelExplorers.Size = new System.Drawing.Size(394, 559);
            this.panelExplorers.TabIndex = 0;
            // 
            // tabExplorers
            // 
            this.tabExplorers.Controls.Add(this.tabPage1);
            this.tabExplorers.Controls.Add(this.tabPage4);
            this.tabExplorers.Controls.Add(this.tabPage2);
            this.tabExplorers.Controls.Add(this.tabPage7);
            this.tabExplorers.Controls.Add(this.tabPage3);
            this.tabExplorers.Controls.Add(this.tabPage8);
            this.tabExplorers.Controls.Add(this.tabPage5);
            this.tabExplorers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabExplorers.Location = new System.Drawing.Point(0, 0);
            this.tabExplorers.Name = "tabExplorers";
            this.tabExplorers.SelectedIndex = 0;
            this.tabExplorers.Size = new System.Drawing.Size(390, 555);
            this.tabExplorers.TabIndex = 0;
            this.tabExplorers.SelectedIndexChanged += new System.EventHandler(this.tabExplorers_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.diskExplorer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(382, 529);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Disk";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // diskExplorer1
            // 
            this.diskExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diskExplorer1.Location = new System.Drawing.Point(3, 3);
            this.diskExplorer1.Name = "diskExplorer1";
            this.diskExplorer1.Size = new System.Drawing.Size(376, 523);
            this.diskExplorer1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.sectorExplorerControl1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(382, 529);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Sectors";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // sectorExplorerControl1
            // 
            this.sectorExplorerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sectorExplorerControl1.Location = new System.Drawing.Point(0, 0);
            this.sectorExplorerControl1.Name = "sectorExplorerControl1";
            this.sectorExplorerControl1.Size = new System.Drawing.Size(382, 529);
            this.sectorExplorerControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.fileExplorer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(382, 529);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Filesystem";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // fileExplorer1
            // 
            this.fileExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileExplorer1.Location = new System.Drawing.Point(3, 3);
            this.fileExplorer1.Name = "fileExplorer1";
            this.fileExplorer1.Size = new System.Drawing.Size(376, 523);
            this.fileExplorer1.TabIndex = 0;
            this.fileExplorer1.StreamSelected += new KFA.GUI.Explorers.StreamSelectedEventHandler(this.fileExplorer1_StreamSelected);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.registryExplorer1);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(382, 529);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Registry";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // registryExplorer1
            // 
            this.registryExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registryExplorer1.Location = new System.Drawing.Point(3, 3);
            this.registryExplorer1.Name = "registryExplorer1";
            this.registryExplorer1.Size = new System.Drawing.Size(376, 523);
            this.registryExplorer1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.historyExplorer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(382, 529);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "History";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // historyExplorer1
            // 
            this.historyExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyExplorer1.Location = new System.Drawing.Point(0, 0);
            this.historyExplorer1.Name = "historyExplorer1";
            this.historyExplorer1.Size = new System.Drawing.Size(382, 529);
            this.historyExplorer1.TabIndex = 0;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.timelineExplorer1);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(382, 529);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Timeline";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // timelineExplorer1
            // 
            this.timelineExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timelineExplorer1.Location = new System.Drawing.Point(0, 0);
            this.timelineExplorer1.Name = "timelineExplorer1";
            this.timelineExplorer1.Size = new System.Drawing.Size(382, 529);
            this.timelineExplorer1.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.searchResultExplorer1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(382, 529);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Search Results";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // searchResultExplorer1
            // 
            this.searchResultExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResultExplorer1.Location = new System.Drawing.Point(0, 0);
            this.searchResultExplorer1.Name = "searchResultExplorer1";
            this.searchResultExplorer1.Size = new System.Drawing.Size(382, 529);
            this.searchResultExplorer1.TabIndex = 0;
            // 
            // panelViewers
            // 
            this.panelViewers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelViewers.Controls.Add(this.tabViewers);
            this.panelViewers.Controls.Add(this.lViewing);
            this.panelViewers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViewers.Location = new System.Drawing.Point(0, 0);
            this.panelViewers.Name = "panelViewers";
            this.panelViewers.Size = new System.Drawing.Size(388, 559);
            this.panelViewers.TabIndex = 6;
            // 
            // tabViewers
            // 
            this.tabViewers.Controls.Add(this.tabPageHex);
            this.tabViewers.Controls.Add(this.tabPageText);
            this.tabViewers.Controls.Add(this.tabPageImage);
            this.tabViewers.Controls.Add(this.tabPageGallery);
            this.tabViewers.Controls.Add(this.tabPageHtml);
            this.tabViewers.Controls.Add(this.tabPage6);
            this.tabViewers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabViewers.Location = new System.Drawing.Point(0, 17);
            this.tabViewers.Name = "tabViewers";
            this.tabViewers.SelectedIndex = 0;
            this.tabViewers.Size = new System.Drawing.Size(384, 538);
            this.tabViewers.TabIndex = 0;
            this.tabViewers.SelectedIndexChanged += new System.EventHandler(this.tabViewers_SelectedIndexChanged);
            // 
            // tabPageHex
            // 
            this.tabPageHex.Controls.Add(this.hexControl1);
            this.tabPageHex.Location = new System.Drawing.Point(4, 22);
            this.tabPageHex.Name = "tabPageHex";
            this.tabPageHex.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHex.Size = new System.Drawing.Size(376, 512);
            this.tabPageHex.TabIndex = 0;
            this.tabPageHex.Text = "Hex";
            this.tabPageHex.UseVisualStyleBackColor = true;
            // 
            // hexControl1
            // 
            this.hexControl1.BackColor = System.Drawing.Color.White;
            this.hexControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexControl1.DataStream = null;
            this.hexControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexControl1.ForeColor = System.Drawing.Color.White;
            this.hexControl1.Location = new System.Drawing.Point(3, 3);
            this.hexControl1.Name = "hexControl1";
            this.hexControl1.Size = new System.Drawing.Size(370, 506);
            this.hexControl1.TabIndex = 0;
            // 
            // tabPageText
            // 
            this.tabPageText.Controls.Add(this.textControl1);
            this.tabPageText.Location = new System.Drawing.Point(4, 22);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageText.Size = new System.Drawing.Size(376, 512);
            this.tabPageText.TabIndex = 1;
            this.tabPageText.Text = "Text";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // textControl1
            // 
            this.textControl1.BackColor = System.Drawing.Color.White;
            this.textControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textControl1.ForeColor = System.Drawing.Color.White;
            this.textControl1.Location = new System.Drawing.Point(3, 3);
            this.textControl1.Name = "textControl1";
            this.textControl1.SelectionEnd = ((ulong)(0ul));
            this.textControl1.SelectionStart = ((ulong)(0ul));
            this.textControl1.Size = new System.Drawing.Size(370, 506);
            this.textControl1.TabIndex = 0;
            // 
            // tabPageImage
            // 
            this.tabPageImage.Controls.Add(this.pictureControl1);
            this.tabPageImage.Location = new System.Drawing.Point(4, 22);
            this.tabPageImage.Name = "tabPageImage";
            this.tabPageImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageImage.Size = new System.Drawing.Size(376, 512);
            this.tabPageImage.TabIndex = 2;
            this.tabPageImage.Text = "Image";
            this.tabPageImage.UseVisualStyleBackColor = true;
            // 
            // pictureControl1
            // 
            this.pictureControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureControl1.Location = new System.Drawing.Point(3, 3);
            this.pictureControl1.Name = "pictureControl1";
            this.pictureControl1.Size = new System.Drawing.Size(370, 506);
            this.pictureControl1.TabIndex = 0;
            // 
            // tabPageGallery
            // 
            this.tabPageGallery.Controls.Add(this.gallery1);
            this.tabPageGallery.Location = new System.Drawing.Point(4, 22);
            this.tabPageGallery.Name = "tabPageGallery";
            this.tabPageGallery.Size = new System.Drawing.Size(376, 512);
            this.tabPageGallery.TabIndex = 4;
            this.tabPageGallery.Text = "Gallery";
            this.tabPageGallery.UseVisualStyleBackColor = true;
            // 
            // gallery1
            // 
            this.gallery1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gallery1.Location = new System.Drawing.Point(0, 0);
            this.gallery1.Name = "gallery1";
            this.gallery1.Size = new System.Drawing.Size(376, 512);
            this.gallery1.TabIndex = 0;
            // 
            // tabPageHtml
            // 
            this.tabPageHtml.Controls.Add(this.webBrowserControl1);
            this.tabPageHtml.Location = new System.Drawing.Point(4, 22);
            this.tabPageHtml.Name = "tabPageHtml";
            this.tabPageHtml.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHtml.Size = new System.Drawing.Size(376, 512);
            this.tabPageHtml.TabIndex = 3;
            this.tabPageHtml.Text = "HTML";
            this.tabPageHtml.UseVisualStyleBackColor = true;
            // 
            // webBrowserControl1
            // 
            this.webBrowserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserControl1.Location = new System.Drawing.Point(3, 3);
            this.webBrowserControl1.Name = "webBrowserControl1";
            this.webBrowserControl1.Size = new System.Drawing.Size(370, 506);
            this.webBrowserControl1.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.histogramControl1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(376, 512);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Histogram";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // histogramControl1
            // 
            this.histogramControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.histogramControl1.Location = new System.Drawing.Point(3, 3);
            this.histogramControl1.Name = "histogramControl1";
            this.histogramControl1.Size = new System.Drawing.Size(370, 506);
            this.histogramControl1.TabIndex = 0;
            // 
            // lViewing
            // 
            this.lViewing.Dock = System.Windows.Forms.DockStyle.Top;
            this.lViewing.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lViewing.Location = new System.Drawing.Point(0, 0);
            this.lViewing.Name = "lViewing";
            this.lViewing.Size = new System.Drawing.Size(384, 17);
            this.lViewing.TabIndex = 1;
            this.lViewing.Text = "Currently viewing:";
            // 
            // CaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 605);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CaseForm";
            this.Text = "KFA Forensics Suite";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panelCase.ResumeLayout(false);
            this.panelExplorers.ResumeLayout(false);
            this.tabExplorers.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panelViewers.ResumeLayout(false);
            this.tabViewers.ResumeLayout(false);
            this.tabPageHex.ResumeLayout(false);
            this.tabPageText.ResumeLayout(false);
            this.tabPageImage.ResumeLayout(false);
            this.tabPageGallery.ResumeLayout(false);
            this.tabPageHtml.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel panelCase;
        private System.Windows.Forms.TreeView caseTreeView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem evidenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAsEvidenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panelViewers;
        private System.Windows.Forms.TabControl tabViewers;
        private System.Windows.Forms.TabPage tabPageHex;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.TabPage tabPageImage;
        private System.Windows.Forms.TabPage tabPageHtml;
        private HexControl hexControl1;
        private TextControl textControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panelExplorers;
        private PictureControl pictureControl1;
        private System.Windows.Forms.TabControl tabExplorers;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private SectorExplorer sectorExplorerControl1;
        private DiskExplorer diskExplorer1;
        private FileExplorer fileExplorer1;
        private System.Windows.Forms.TabPage tabPage3;
        private HistoryExplorer historyExplorer1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem textHexSearchToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageGallery;
        private Gallery gallery1;
        private WebBrowserControl webBrowserControl1;
        private System.Windows.Forms.TabPage tabPage5;
        private SearchResultExplorer searchResultExplorer1;
        private System.Windows.Forms.ToolStripMenuItem userDocsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSearchToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ToolStripMenuItem caseInfoToolStripMenuItem;
        private HistogramControl histogramControl1;
        private System.Windows.Forms.ToolStripMenuItem recentCasesToolStripMenuItem;
        private System.Windows.Forms.Label lViewing;
        private System.Windows.Forms.TabPage tabPage7;
        private RegistryExplorer registryExplorer1;
        private System.Windows.Forms.TabPage tabPage8;
        private TimelineExplorer timelineExplorer1;
        //private ForensicsApp.GUI.Viewers.HistogramControl histogramControl1;
    }
}

