using System.Collections.Generic;

namespace WiFiHeatMap.Server
{
    public interface ISignalParser
    {
        IEnumerable<Signal> Parse(string results);
    }
}