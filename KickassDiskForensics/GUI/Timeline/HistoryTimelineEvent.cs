using System;
using KFA.ApplicationLevel.History;
using KFA.DataStream;

namespace KFA.GUI.Timeline {
    public class HistoryTimelineEvent : ITimelineEvent {
        private ExplorerHistoryRecord m_Record;
        public HistoryTimelineEvent(ExplorerHistoryRecord record) {
            m_Record = record;
        }

        public DateTime Time {
            get { return m_Record.LastAccessed; }
        }

        public EventType Type {
            get { return EventType.WebsiteVisited; }
        }

        public string Description {
            get { return m_Record.Description; }
        }

        public IDataStream DataStream {
            get { return m_Record.DataStream; }
        }
    }
}
