namespace KFA.GUI {
    partial class FileSearchForm {
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
            this.button1 = new System.Windows.Forms.Button();
            this.cbDeleted = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxTypeFilter = new System.Windows.Forms.TextBox();
            this.comboBoxFileFilters = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbAltStreams = new System.Windows.Forms.CheckBox();
            this.cbHiddenFiles = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(177, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Find";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbDeleted
            // 
            this.cbDeleted.AutoSize = true;
            this.cbDeleted.Location = new System.Drawing.Point(14, 21);
            this.cbDeleted.Name = "cbDeleted";
            this.cbDeleted.Size = new System.Drawing.Size(92, 17);
            this.cbDeleted.TabIndex = 1;
            this.cbDeleted.Text = "Deleted Files";
            this.cbDeleted.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxTypeFilter);
            this.groupBox1.Controls.Add(this.comboBoxFileFilters);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 82);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Type";
            // 
            // textBoxTypeFilter
            // 
            this.textBoxTypeFilter.Enabled = false;
            this.textBoxTypeFilter.Location = new System.Drawing.Point(14, 51);
            this.textBoxTypeFilter.Name = "textBoxTypeFilter";
            this.textBoxTypeFilter.Size = new System.Drawing.Size(173, 22);
            this.textBoxTypeFilter.TabIndex = 1;
            // 
            // comboBoxFileFilters
            // 
            this.comboBoxFileFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFileFilters.FormattingEnabled = true;
            this.comboBoxFileFilters.Location = new System.Drawing.Point(14, 24);
            this.comboBoxFileFilters.Name = "comboBoxFileFilters";
            this.comboBoxFileFilters.Size = new System.Drawing.Size(102, 21);
            this.comboBoxFileFilters.TabIndex = 0;
            this.comboBoxFileFilters.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFileFilters_SelectionChangeCommitted);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbAltStreams);
            this.groupBox2.Controls.Add(this.cbHiddenFiles);
            this.groupBox2.Controls.Add(this.cbDeleted);
            this.groupBox2.Location = new System.Drawing.Point(12, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 104);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Special";
            // 
            // cbAltStreams
            // 
            this.cbAltStreams.AutoSize = true;
            this.cbAltStreams.Location = new System.Drawing.Point(14, 67);
            this.cbAltStreams.Name = "cbAltStreams";
            this.cbAltStreams.Size = new System.Drawing.Size(204, 17);
            this.cbAltStreams.TabIndex = 5;
            this.cbAltStreams.Text = "Alternate Data Streams (NTFS Only)";
            this.cbAltStreams.UseVisualStyleBackColor = true;
            // 
            // cbHiddenFiles
            // 
            this.cbHiddenFiles.AutoSize = true;
            this.cbHiddenFiles.Location = new System.Drawing.Point(14, 44);
            this.cbHiddenFiles.Name = "cbHiddenFiles";
            this.cbHiddenFiles.Size = new System.Drawing.Size(90, 17);
            this.cbHiddenFiles.TabIndex = 4;
            this.cbHiddenFiles.Text = "Hidden Files";
            this.cbHiddenFiles.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(245, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 26);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FileSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 270);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "FileSearchForm";
            this.Text = "File Search";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbDeleted;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxFileFilters;
        private System.Windows.Forms.TextBox textBoxTypeFilter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbAltStreams;
        private System.Windows.Forms.CheckBox cbHiddenFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
    }
}