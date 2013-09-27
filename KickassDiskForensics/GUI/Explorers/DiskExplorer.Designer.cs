namespace KFA.GUI.Explorers {
    partial class DiskExplorer {
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
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.partitionDiagram1 = new PartitionDiagram();
            this.SuspendLayout();
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.Location = new System.Drawing.Point(3, 51);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDescription.Size = new System.Drawing.Size(517, 449);
            this.tbDescription.TabIndex = 4;
            // 
            // partitionDiagram1
            // 
            this.partitionDiagram1.ActiveSection = null;
            this.partitionDiagram1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.partitionDiagram1.Disk = null;
            this.partitionDiagram1.Location = new System.Drawing.Point(3, 3);
            this.partitionDiagram1.Name = "partitionDiagram1";
            this.partitionDiagram1.Size = new System.Drawing.Size(517, 42);
            this.partitionDiagram1.TabIndex = 5;
            // 
            // DiskExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.partitionDiagram1);
            this.Controls.Add(this.tbDescription);
            this.Name = "DiskExplorer";
            this.Size = new System.Drawing.Size(523, 503);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDescription;
        private PartitionDiagram partitionDiagram1;
    }
}
