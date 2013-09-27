namespace KFA.GUI.Explorers {
    partial class PartitionDiagram {
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
            this.panelDiagram = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelDiagram
            // 
            this.panelDiagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDiagram.Location = new System.Drawing.Point(0, 0);
            this.panelDiagram.Name = "panelDiagram";
            this.panelDiagram.Size = new System.Drawing.Size(670, 163);
            this.panelDiagram.TabIndex = 6;
            // 
            // PartitionDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDiagram);
            this.Name = "PartitionDiagram";
            this.Size = new System.Drawing.Size(670, 163);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDiagram;
    }
}
