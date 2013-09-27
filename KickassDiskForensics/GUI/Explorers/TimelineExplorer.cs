using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KFA.DataStream;
using KFA.Disks;
using KFA.GUI.Timeline;

namespace KFA.GUI.Explorers {
    public partial class TimelineExplorer : UserControl, IExplorer {
        private IDataStream m_CurrentStream = null;
        private TimelineEventStore m_EventStore = null;
        public TimelineExplorer() {
            InitializeComponent();
            UpdateGUI();
        }

        #region IExplorer Members

        public bool CanView(IDataStream stream) {
            return stream is IFileSystemStore;
        }

        public void View(IDataStream stream) {
            if (stream != m_CurrentStream) {
                m_CurrentStream = stream;
                if (((IFileSystemStore)stream).FS != null) {
                    m_EventStore = new TimelineEventStore(((IFileSystemStore)stream).FS);
                }

                UpdateGUI();
            }
        }

        #endregion

        private void dateRangeBar1_RangeChanging(object sender, EventArgs e) {
            timelineControl1.SetRange(dateRangeBar1.RangeMinimum, dateRangeBar1.RangeMaximum, false);
        }

        private void dateRangeBar1_RangeChanged(object sender, EventArgs e) {
            timelineControl1.SetRange(dateRangeBar1.RangeMinimum, dateRangeBar1.RangeMaximum, true);
        }

        private void UpdateGUI() {
            if (m_EventStore == null) {
                labelLoad.Visible = false;
                timelineControl1.Visible = false;
                dateRangeBar1.Visible = false;
            } else {
                if (m_EventStore.Loaded) {
                    labelLoad.Visible = false;
                    timelineControl1.Visible = true;
                    dateRangeBar1.Visible = true;
                    timelineControl1.EventStore = m_EventStore;
                    dateRangeBar1.TotalMaximum = DateTime.Now.AddDays(2);
                    dateRangeBar1.TotalMinimum = DateTime.Now.AddDays(-100);
                    dateRangeBar1.RangeMaximum = DateTime.Now;
                    dateRangeBar1.RangeMinimum = DateTime.Now.AddDays(-7);
                    dateRangeBar1_RangeChanged(null, null);
                } else {
                    labelLoad.Visible = true;
                    timelineControl1.Visible = false;
                    dateRangeBar1.Visible = false;
                    timelineControl1.EventStore = null;
                }
            }
        }

        private void labelLoad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (m_EventStore != null && !m_EventStore.Loaded) {
                labelLoad.Visible = false;
                m_EventStore.Load(delegate(double percentage, string s) {
                    Console.WriteLine("{0:0}% {1}", percentage * 100, s);
                },
                delegate() {
                    Invoke(new Action(delegate() {
                        UpdateGUI();
                    }));
                });
            }
        }

        private void timelineControl1_ActivityPeriodSelected(object sender, EventArgs e) {
            activityPeriodViewer1.ActivityPeriod = sender as ActivityPeriod;
        }
    }
}
