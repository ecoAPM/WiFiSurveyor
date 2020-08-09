using System.Collections.Generic;

namespace WiFiHeatMap
{
    public interface ISignalParser<in T>
    {
        IList<Signal> Parse(T results);
    }
}