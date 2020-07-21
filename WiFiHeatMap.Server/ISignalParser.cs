using System.Collections.Generic;

namespace WiFiHeatMap.Server
{
    public interface ISignalParser<in T>
    {
        IList<Signal> Parse(T results);
    }
}