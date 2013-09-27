using System;
using KFA.DataStream;

namespace KFA.GUI.Timeline {
    public enum EventType {
        FileCreated,
        FileModified,
        RegistryKeyCreated,
        RegistryKeyModified,
        WebsiteVisited,
        CookieReceived
    }
    public interface ITimelineEvent {
        DateTime Time { get; }
        EventType Type { get; }
        string Description { get; }
        IDataStream DataStream { get; }
    }
}
