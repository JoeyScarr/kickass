namespace KFA.GUI.Explorers {
    partial class RegistryExplorer {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistryExplorer));
            this.treeKeys = new System.Windows.Forms.TreeView();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.radioViewFull = new System.Windows.Forms.RadioButton();
            this.radioViewSimple = new System.Windows.Forms.RadioButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeKeys
            // 
            this.treeKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeKeys.HideSelection = false;
            this.treeKeys.Location = new System.Drawing.Point(0, 0);
            this.treeKeys.Name = "treeKeys";
            this.treeKeys.Size = new System.Drawing.Size(200, 405);
            this.treeKeys.TabIndex = 2;
            this.treeKeys.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeFiles_BeforeExpand);
            this.treeKeys.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFiles_AfterSelect);
            // 
            // cmbUser
            // 
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUser.Enabled = false;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(181, 3);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(145, 21);
            this.cmbUser.TabIndex = 4;
            this.cmbUser.SelectedValueChanged += new System.EventHandler(this.cmbUser_SelectedValueChanged);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(276, 405);
            this.listView.TabIndex = 6;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 201;
            // 
            // radioViewFull
            // 
            this.radioViewFull.AutoSize = true;
            this.radioViewFull.Location = new System.Drawing.Point(87, 4);
            this.radioViewFull.Name = "radioViewFull";
            this.radioViewFull.Size = new System.Drawing.Size(88, 17);
            this.radioViewFull.TabIndex = 7;
            this.radioViewFull.Text = "View As User";
            this.radioViewFull.UseVisualStyleBackColor = true;
            this.radioViewFull.CheckedChanged += new System.EventHandler(this.radioViewFull_CheckedChanged);
            // 
            // radioViewSimple
            // 
            this.radioViewSimple.AutoSize = true;
            this.radioViewSimple.Checked = true;
            this.radioViewSimple.Location = new System.Drawing.Point(3, 4);
            this.radioViewSimple.Name = "radioViewSimple";
            this.radioViewSimple.Size = new System.Drawing.Size(78, 17);
            this.radioViewSimple.TabIndex = 8;
            this.radioViewSimple.TabStop = true;
            this.radioViewSimple.Text = "View Hives";
            this.radioViewSimple.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Key");
            this.imageList1.Images.SetKeyName(1, "Hive");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 30);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeKeys);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView);
            this.splitContainer1.Size = new System.Drawing.Size(480, 405);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 9;
            // 
            // RegistryExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.radioViewSimple);
            this.Controls.Add(this.radioViewFull);
            this.Controls.Add(this.cmbUser);
            this.Name = "RegistryExplorer";
            this.Size = new System.Drawing.Size(486, 438);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeKeys;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.RadioButton radioViewFull;
        private System.Windows.Forms.RadioButton radioViewSimple;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
