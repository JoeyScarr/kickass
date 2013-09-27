using System.Windows.Forms;
using System.Collections.Generic;

namespace KFA.GUI.Timeline {
    public partial class ActivityPeriodViewer : UserControl {
        private ActivityPeriod m_Period;
        private static Dictionary<Granularity, string> m_DateFormats = new Dictionary<Granularity, string>() {
                                            { Granularity.Hour, "h tt" },
                                            { Granularity.Day, "yyyy-MM-dd" },
                                            { Granularity.Month, "yyyy-MM-dd" } };
        private static Dictionary<Granularity, string> m_TimeFormats = new Dictionary<Granularity, string>() {
                                            { Granularity.Hour, "HH:mm:ss tt" },
                                            { Granularity.Day, "yyyy-MM-dd HH:mm" },
                                            { Granularity.Month, "yyyy-MM-dd HH:mm" } };

        public ActivityPeriodViewer() {
            InitializeComponent();
        }

        public ActivityPeriod ActivityPeriod {
            get { return m_Period; }
            set {
                if (m_Period != value) {
                    m_Period = value;
                    listView1.Items.Clear();
                    if (m_Period == null) {
                        lDateRange.Text = "";
                    } else {
                        List<ITimelineEvent> events = m_Period.GetEvents();
                        lDateRange.Text = string.Format("{0} to {1}: {2} items",
                            m_Period.Start.ToString(m_DateFormats[m_Period.Granularity]),
                            m_Period.End.ToString(m_DateFormats[m_Period.Granularity]), events.Count);
                        foreach (ITimelineEvent ev in events) {
                            ListViewItem lvi = new ListViewItem(new string[] { ev.Time.ToString(m_TimeFormats[m_Period.Granularity]), ev.Description });
                            lvi.Tag = ev;
                            listView1.Items.Add(lvi);
                        }
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, System.EventArgs e) {
            if (listView1.SelectedItems.Count > 0) {
                ITimelineEvent ev = listView1.SelectedItems[0].Tag as ITimelineEvent;
                if (ev != null && ev.DataStream != null) {
                    CaseForm.Instance.ViewableObject = ev.DataStream;
                }
            }
        }
    }
}
