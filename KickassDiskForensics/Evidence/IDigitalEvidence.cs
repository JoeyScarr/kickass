using System;
using KFA.DataStream;

namespace KFA.Evidence {
    public interface IDigitalEvidence : IDataStream {
        String Name { get; set; }
    }
}
