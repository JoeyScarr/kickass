using System;
using KFS.DataStream;

namespace KFA.Evidence {
    public interface IDigitalEvidence : IDataStream {
        String Name { get; set; }
    }
}
