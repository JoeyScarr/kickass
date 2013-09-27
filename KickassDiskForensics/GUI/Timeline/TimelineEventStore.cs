using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KFA.ApplicationLevel.History;
using FileSystems.FileSystem;

namespace KFA.GUI.Timeline {
    public enum Granularity {
        Hour, Day, Month
    }
    public class TimelineEventStore {
        private const long TICKS_PER_HOUR = 36000000000;
        private const long TICKS_PER_DAY = TICKS_PER_HOUR * 24;

        private FileSystem m_FileSystem;
        private bool m_Loaded = false;

        public TimelineEventStore(FileSystem fileSystem) {
            m_FileSystem = fileSystem;
        }

        private List<ITimelineEvent> m_TimelineEvents = new List<ITimelineEvent>();
        private HashSet<long> m_HourlyActivity = new HashSet<long>();
        private HashSet<long> m_DailyActivity = new HashSet<long>();
        private HashSet<long> m_MonthlyActivity = new HashSet<long>();

        private bool ActivityDuringHour(DateTime time) {
            return m_HourlyActivity.Contains(time.Ticks / TICKS_PER_HOUR * TICKS_PER_HOUR);
        }
        private bool ActivityDuringDay(DateTime time) {
            return m_DailyActivity.Contains(time.Ticks / TICKS_PER_DAY * TICKS_PER_DAY);
        }
        private bool ActivityDuringMonth(DateTime time) {
            return m_MonthlyActivity.Contains(new DateTime(time.Year, time.Month, 1).Ticks);
        }

        public bool Loaded {
            get { return m_Loaded; }
        }

        private void AddEvent(ITimelineEvent ev) {
            m_HourlyActivity.Add(ev.Time.Ticks / TICKS_PER_HOUR * TICKS_PER_HOUR);
            m_DailyActivity.Add(ev.Time.Ticks / TICKS_PER_DAY * TICKS_PER_DAY);
            m_MonthlyActivity.Add(new DateTime(ev.Time.Year, ev.Time.Month, 1).Ticks);
            m_TimelineEvents.Add(ev);
        }

        private void LoadWebHistory(Action<double, string> progressDelegate) {
            int i = 0;
            foreach (ExplorerHistoryFile file in History.GetExplorerHistoryFiles(m_FileSystem)) {
                foreach (ExplorerHistoryRecord record in file.GetRecords()) {
                    AddEvent(new HistoryTimelineEvent(record));
                }
                progressDelegate(Math.Min(1.0, (double)i / ((double)History.NumExplorerHistoryFiles * 2)),
                    "Reading " + file.Path);
                i++;
            }
        }

        public void Load(Action<double, string> progressDelegate, Action finishDelegate) {
            Thread t = new Thread(delegate() {
                int numEventTypes = 1;
                progressDelegate(0, "Loading...");

                LoadWebHistory(delegate(double percent, string desc) {
                    progressDelegate(percent / (double)numEventTypes, desc);
                });

                m_Loaded = true;
                progressDelegate(1, "Done!");
                finishDelegate();
            });
            t.Start();
        }

        public List<ActivityPeriod> GetActivity(DateTime start, DateTime end, Granularity gran) {
            List<ActivityPeriod> res = new List<ActivityPeriod>();
            DateTime currentStart = start;
            DateTime current = start;
            bool on = false;
            while (current <= end) {
                bool onHere = false;
                DateTime now = current;
                switch (gran) {
                    case Granularity.Hour:
                        onHere = ActivityDuringHour(current);
                        current = current.AddHours(1);
                        break;
                    case Granularity.Day:
                        onHere = ActivityDuringDay(current);
                        current = current.AddDays(1);
                        break;
                    case Granularity.Month:
                        onHere = ActivityDuringMonth(current);
                        current = current.AddMonths(1);
                        break;
                }
                if (!on && onHere) {
                    on = true;
                    currentStart = now;
                } else if (on && !onHere) {
                    on = false;
                    res.Add(new ActivityPeriod(currentStart, now, gran, this));
                }
            }
            if (on) {
                res.Add(new ActivityPeriod(currentStart, end, gran, this));
            }

            return res;
        }

        public IEnumerable<ITimelineEvent> GetEvents(DateTime start, DateTime end) {
            return m_TimelineEvents.Where(delegate(ITimelineEvent ev) {
                return ev.Time >= start && ev.Time <= end;
            });
        }
    }
}
