using KFA.GUI.Timeline;

namespace KFA.GUI.Explorers {
    partial class TimelineExplorer {
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelLoad = new System.Windows.Forms.LinkLabel();
            this.dateRangeBar1 = new KFA.GUI.Timeline.DateRangeBar();
            this.timelineControl1 = new KFA.GUI.Timeline.TimelineControl();
            this.activityPeriodViewer1 = new KFA.GUI.Timeline.ActivityPeriodViewer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.timelineControl1);
            this.splitContainer1.Panel1.Controls.Add(this.labelLoad);
            this.splitContainer1.Panel1.Controls.Add(this.dateRangeBar1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.activityPeriodViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(654, 515);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.TabIndex = 1;
            // 
            // labelLoad
            // 
            this.labelLoad.AutoSize = true;
            this.labelLoad.Location = new System.Drawing.Point(3, 2);
            this.labelLoad.Name = "labelLoad";
            this.labelLoad.Size = new System.Drawing.Size(104, 13);
            this.labelLoad.TabIndex = 3;
            this.labelLoad.TabStop = true;
            this.labelLoad.Text = "Load timeline events";
            this.labelLoad.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelLoad_LinkClicked);
            // 
            // dateRangeBar1
            // 
            this.dateRangeBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateRangeBar1.DivisionNum = 10;
            this.dateRangeBar1.HeightOfBar = 8;
            this.dateRangeBar1.HeightOfMark = 24;
            this.dateRangeBar1.HeightOfTick = 6;
            this.dateRangeBar1.InnerColor = System.Drawing.Color.LightGreen;
            this.dateRangeBar1.Location = new System.Drawing.Point(-2, 278);
            this.dateRangeBar1.Name = "dateRangeBar1";
            this.dateRangeBar1.Orientation = KFA.GUI.Timeline.DateRangeBar.RangeBarOrientation.Horizontal;
            this.dateRangeBar1.RangeMaximum = new System.DateTime(2010, 6, 14, 19, 46, 18, 343);
            this.dateRangeBar1.RangeMinimum = new System.DateTime(2010, 6, 17, 14, 31, 19, 494);
            this.dateRangeBar1.ScaleOrientation = KFA.GUI.Timeline.DateRangeBar.TopBottomOrientation.Bottom;
            this.dateRangeBar1.Size = new System.Drawing.Size(653, 63);
            this.dateRangeBar1.TabIndex = 2;
            this.dateRangeBar1.TotalMaximum = new System.DateTime(2010, 6, 14, 19, 46, 18, 343);
            this.dateRangeBar1.TotalMinimum = new System.DateTime(2010, 6, 13, 19, 46, 18, 343);
            this.dateRangeBar1.RangeChanged += new KFA.GUI.Timeline.DateRangeBar.RangeChangedEventHandler(this.dateRangeBar1_RangeChanged);
            this.dateRangeBar1.RangeChanging += new KFA.GUI.Timeline.DateRangeBar.RangeChangedEventHandler(this.dateRangeBar1_RangeChanging);
            // 
            // timelineControl1
            // 
            this.timelineControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timelineControl1.EventStore = null;
            this.timelineControl1.HeaderHeight = 18;
            this.timelineControl1.LeftColumnWidth = 60;
            this.timelineControl1.Location = new System.Drawing.Point(-1, -1);
            this.timelineControl1.Name = "timelineControl1";
            this.timelineControl1.Size = new System.Drawing.Size(652, 291);
            this.timelineControl1.TabIndex = 1;
            this.timelineControl1.ActivityPeriodSelected += new System.EventHandler(this.timelineControl1_ActivityPeriodSelected);
            // 
            // activityPeriodViewer1
            // 
            this.activityPeriodViewer1.ActivityPeriod = null;
            this.activityPeriodViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityPeriodViewer1.Location = new System.Drawing.Point(0, 0);
            this.activityPeriodViewer1.Name = "activityPeriodViewer1";
            this.activityPeriodViewer1.Size = new System.Drawing.Size(650, 164);
            this.activityPeriodViewer1.TabIndex = 0;
            // 
            // TimelineExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TimelineExplorer";
            this.Size = new System.Drawing.Size(654, 515);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private TimelineControl timelineControl1;
        private DateRangeBar dateRangeBar1;
        private System.Windows.Forms.LinkLabel labelLoad;
        private ActivityPeriodViewer activityPeriodViewer1;

    }
}
