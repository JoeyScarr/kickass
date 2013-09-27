namespace KFA.GUI.Viewers {
    partial class TextControl {
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
            this.txtBoxAscii = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.bigScrollbar1 = new BigScrollbar();
            this.SuspendLayout();
            // 
            // txtBoxAscii
            // 
            this.txtBoxAscii.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxAscii.BackColor = System.Drawing.Color.White;
            this.txtBoxAscii.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxAscii.DetectUrls = false;
            this.txtBoxAscii.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxAscii.HideSelection = false;
            this.txtBoxAscii.Location = new System.Drawing.Point(125, 3);
            this.txtBoxAscii.Name = "txtBoxAscii";
            this.txtBoxAscii.ReadOnly = true;
            this.txtBoxAscii.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtBoxAscii.Size = new System.Drawing.Size(404, 463);
            this.txtBoxAscii.TabIndex = 3;
            this.txtBoxAscii.Text = "";
            this.txtBoxAscii.Resize += new System.EventHandler(this.txtBoxAscii_Resize);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(116, 462);
            this.listBox1.TabIndex = 7;
            // 
            // bigScrollbar1
            // 
            this.bigScrollbar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bigScrollbar1.LargeChange = ((long)(10));
            this.bigScrollbar1.Location = new System.Drawing.Point(532, 25);
            this.bigScrollbar1.Maximum = ((long)(100));
            this.bigScrollbar1.Minimum = ((long)(0));
            this.bigScrollbar1.Name = "bigScrollbar1";
            this.bigScrollbar1.Size = new System.Drawing.Size(19, 441);
            this.bigScrollbar1.SmallChange = ((long)(1));
            this.bigScrollbar1.TabIndex = 8;
            this.bigScrollbar1.Value = ((long)(0));
            this.bigScrollbar1.BigScroll += new BigScrollbar.BigScrollEventHandler(this.bigScrollbar1_BigScroll);
            // 
            // TextControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.bigScrollbar1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtBoxAscii);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "TextControl";
            this.Size = new System.Drawing.Size(556, 469);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtBoxAscii;
        private System.Windows.Forms.ListBox listBox1;
        private BigScrollbar bigScrollbar1;
    }
}
