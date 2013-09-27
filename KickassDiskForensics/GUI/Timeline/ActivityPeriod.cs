using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace KFA.GUI.Timeline {
    public class ActivityPeriod : Control {
        private TimelineEventStore m_Store;
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public Granularity Granularity { get; private set; }
        public TimeSpan Length {
            get {
                return End - Start;
            }
        }
        public ActivityPeriod(DateTime start, DateTime end, Granularity gran, TimelineEventStore store) {
            Start = start;
            End = end;
            Granularity = gran;
            m_Store = store;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        List<ITimelineEvent> m_Events = null;
        private void LoadEvents() {
            m_Events = new List<ITimelineEvent>();
            m_Events.AddRange(m_Store.GetEvents(Start, End));
        }

        public List<ITimelineEvent> GetEvents() {
            LoadEvents();
            return m_Events;
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        public override bool Equals(object obj) {
            if (obj != null && obj is ActivityPeriod) {
                ActivityPeriod other = obj as ActivityPeriod;
                return Start == other.Start && End == other.End;
            }
            return false;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
