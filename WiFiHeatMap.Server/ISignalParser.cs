using System.Collections.Generic;

namespace WiFiHeatMap.Server
{
    public interface ISignalParser
    {
        IList<Signal> Parse(string results);
    }
}