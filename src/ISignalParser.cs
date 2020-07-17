using System.Collections.Generic;

namespace WiFiHeatMap
{
    public interface ISignalParser
    {
        IEnumerable<Signal> Parse(string results);
    }
}