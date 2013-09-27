namespace KFA.GUI.Viewers {
    partial class HexControl {
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
            this.txtBoxHex = new System.Windows.Forms.RichTextBox();
            this.txtBoxOffsetH = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.numbersPanel = new System.Windows.Forms.Panel();
            this.tbUnicode = new System.Windows.Forms.TextBox();
            this.tbAscii = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbUint64 = new System.Windows.Forms.TextBox();
            this.tbInt64 = new System.Windows.Forms.TextBox();
            this.tbUint32 = new System.Windows.Forms.TextBox();
            this.tbInt32 = new System.Windows.Forms.TextBox();
            this.tbUint16 = new System.Windows.Forms.TextBox();
            this.tbInt16 = new System.Windows.Forms.TextBox();
            this.tbByte = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bigScrollbar1 = new BigScrollbar();
            this.numbersPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxAscii
            // 
            this.txtBoxAscii.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBoxAscii.BackColor = System.Drawing.Color.White;
            this.txtBoxAscii.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxAscii.DetectUrls = false;
            this.txtBoxAscii.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxAscii.HideSelection = false;
            this.txtBoxAscii.Location = new System.Drawing.Point(471, 25);
            this.txtBoxAscii.Name = "txtBoxAscii";
            this.txtBoxAscii.ReadOnly = true;
            this.txtBoxAscii.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtBoxAscii.Size = new System.Drawing.Size(122, 392);
            this.txtBoxAscii.TabIndex = 3;
            this.txtBoxAscii.Text = "";
            this.txtBoxAscii.SelectionChanged += new System.EventHandler(this.txtBoxAscii_SelectionChanged);
            // 
            // txtBoxHex
            // 
            this.txtBoxHex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBoxHex.BackColor = System.Drawing.Color.White;
            this.txtBoxHex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxHex.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxHex.HideSelection = false;
            this.txtBoxHex.Location = new System.Drawing.Point(128, 25);
            this.txtBoxHex.Name = "txtBoxHex";
            this.txtBoxHex.ReadOnly = true;
            this.txtBoxHex.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtBoxHex.Size = new System.Drawing.Size(337, 392);
            this.txtBoxHex.TabIndex = 2;
            this.txtBoxHex.Text = "";
            this.txtBoxHex.SelectionChanged += new System.EventHandler(this.txtBoxHex_SelectionChanged);
            // 
            // txtBoxOffsetH
            // 
            this.txtBoxOffsetH.BackColor = System.Drawing.Color.White;
            this.txtBoxOffsetH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxOffsetH.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.txtBoxOffsetH.Location = new System.Drawing.Point(128, 3);
            this.txtBoxOffsetH.Name = "txtBoxOffsetH";
            this.txtBoxOffsetH.ReadOnly = true;
            this.txtBoxOffsetH.Size = new System.Drawing.Size(337, 13);
            this.txtBoxOffsetH.TabIndex = 5;
            this.txtBoxOffsetH.Text = "0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(3, 25);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(119, 378);
            this.listBox1.TabIndex = 7;
            // 
            // numbersPanel
            // 
            this.numbersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numbersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numbersPanel.Controls.Add(this.tbUnicode);
            this.numbersPanel.Controls.Add(this.tbAscii);
            this.numbersPanel.Controls.Add(this.label8);
            this.numbersPanel.Controls.Add(this.label9);
            this.numbersPanel.Controls.Add(this.tbUint64);
            this.numbersPanel.Controls.Add(this.tbInt64);
            this.numbersPanel.Controls.Add(this.tbUint32);
            this.numbersPanel.Controls.Add(this.tbInt32);
            this.numbersPanel.Controls.Add(this.tbUint16);
            this.numbersPanel.Controls.Add(this.tbInt16);
            this.numbersPanel.Controls.Add(this.tbByte);
            this.numbersPanel.Controls.Add(this.label7);
            this.numbersPanel.Controls.Add(this.label6);
            this.numbersPanel.Controls.Add(this.label5);
            this.numbersPanel.Controls.Add(this.label4);
            this.numbersPanel.Controls.Add(this.label3);
            this.numbersPanel.Controls.Add(this.label2);
            this.numbersPanel.Controls.Add(this.label1);
            this.numbersPanel.Location = new System.Drawing.Point(-1, 423);
            this.numbersPanel.Name = "numbersPanel";
            this.numbersPanel.Size = new System.Drawing.Size(642, 82);
            this.numbersPanel.TabIndex = 9;
            // 
            // tbUnicode
            // 
            this.tbUnicode.ForeColor = System.Drawing.Color.Black;
            this.tbUnicode.Location = new System.Drawing.Point(386, 55);
            this.tbUnicode.Name = "tbUnicode";
            this.tbUnicode.ReadOnly = true;
            this.tbUnicode.Size = new System.Drawing.Size(118, 20);
            this.tbUnicode.TabIndex = 33;
            // 
            // tbAscii
            // 
            this.tbAscii.ForeColor = System.Drawing.Color.Black;
            this.tbAscii.Location = new System.Drawing.Point(211, 55);
            this.tbAscii.Name = "tbAscii";
            this.tbAscii.ReadOnly = true;
            this.tbAscii.Size = new System.Drawing.Size(118, 20);
            this.tbAscii.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(335, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "unicode";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(169, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "string";
            // 
            // tbUint64
            // 
            this.tbUint64.ForeColor = System.Drawing.Color.Black;
            this.tbUint64.Location = new System.Drawing.Point(386, 29);
            this.tbUint64.Name = "tbUint64";
            this.tbUint64.ReadOnly = true;
            this.tbUint64.Size = new System.Drawing.Size(118, 20);
            this.tbUint64.TabIndex = 29;
            // 
            // tbInt64
            // 
            this.tbInt64.ForeColor = System.Drawing.Color.Black;
            this.tbInt64.Location = new System.Drawing.Point(386, 3);
            this.tbInt64.Name = "tbInt64";
            this.tbInt64.ReadOnly = true;
            this.tbInt64.Size = new System.Drawing.Size(118, 20);
            this.tbInt64.TabIndex = 28;
            // 
            // tbUint32
            // 
            this.tbUint32.ForeColor = System.Drawing.Color.Black;
            this.tbUint32.Location = new System.Drawing.Point(211, 29);
            this.tbUint32.Name = "tbUint32";
            this.tbUint32.ReadOnly = true;
            this.tbUint32.Size = new System.Drawing.Size(118, 20);
            this.tbUint32.TabIndex = 27;
            // 
            // tbInt32
            // 
            this.tbInt32.ForeColor = System.Drawing.Color.Black;
            this.tbInt32.Location = new System.Drawing.Point(211, 3);
            this.tbInt32.Name = "tbInt32";
            this.tbInt32.ReadOnly = true;
            this.tbInt32.Size = new System.Drawing.Size(118, 20);
            this.tbInt32.TabIndex = 26;
            // 
            // tbUint16
            // 
            this.tbUint16.ForeColor = System.Drawing.Color.Black;
            this.tbUint16.Location = new System.Drawing.Point(45, 29);
            this.tbUint16.Name = "tbUint16";
            this.tbUint16.ReadOnly = true;
            this.tbUint16.Size = new System.Drawing.Size(118, 20);
            this.tbUint16.TabIndex = 25;
            // 
            // tbInt16
            // 
            this.tbInt16.ForeColor = System.Drawing.Color.Black;
            this.tbInt16.Location = new System.Drawing.Point(45, 3);
            this.tbInt16.Name = "tbInt16";
            this.tbInt16.ReadOnly = true;
            this.tbInt16.Size = new System.Drawing.Size(118, 20);
            this.tbInt16.TabIndex = 24;
            // 
            // tbByte
            // 
            this.tbByte.ForeColor = System.Drawing.Color.Black;
            this.tbByte.Location = new System.Drawing.Point(45, 55);
            this.tbByte.Name = "tbByte";
            this.tbByte.ReadOnly = true;
            this.tbByte.Size = new System.Drawing.Size(118, 20);
            this.tbByte.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(335, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "uint64";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(335, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "int64";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(169, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "uint32";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(169, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "int32";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "uint16";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "int16";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "byte";
            // 
            // bigScrollbar1
            // 
            this.bigScrollbar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bigScrollbar1.LargeChange = ((long)(10));
            this.bigScrollbar1.Location = new System.Drawing.Point(619, 0);
            this.bigScrollbar1.Maximum = ((long)(100));
            this.bigScrollbar1.Minimum = ((long)(0));
            this.bigScrollbar1.Name = "bigScrollbar1";
            this.bigScrollbar1.Size = new System.Drawing.Size(19, 421);
            this.bigScrollbar1.SmallChange = ((long)(1));
            this.bigScrollbar1.TabIndex = 8;
            this.bigScrollbar1.Value = ((long)(0));
            this.bigScrollbar1.BigScroll += new BigScrollbar.BigScrollEventHandler(this.bigScrollbar1_BigScroll);
            // 
            // HexControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.numbersPanel);
            this.Controls.Add(this.bigScrollbar1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtBoxOffsetH);
            this.Controls.Add(this.txtBoxAscii);
            this.Controls.Add(this.txtBoxHex);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "HexControl";
            this.Size = new System.Drawing.Size(640, 504);
            this.Resize += new System.EventHandler(this.HexControl_Resize);
            this.numbersPanel.ResumeLayout(false);
            this.numbersPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtBoxAscii;
        private System.Windows.Forms.RichTextBox txtBoxHex;
        private System.Windows.Forms.TextBox txtBoxOffsetH;
        private System.Windows.Forms.ListBox listBox1;
        private BigScrollbar bigScrollbar1;
        private System.Windows.Forms.Panel numbersPanel;
        private System.Windows.Forms.TextBox tbUint64;
        private System.Windows.Forms.TextBox tbInt64;
        private System.Windows.Forms.TextBox tbUint32;
        private System.Windows.Forms.TextBox tbInt32;
        private System.Windows.Forms.TextBox tbUint16;
        private System.Windows.Forms.TextBox tbInt16;
        private System.Windows.Forms.TextBox tbByte;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUnicode;
        private System.Windows.Forms.TextBox tbAscii;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}
