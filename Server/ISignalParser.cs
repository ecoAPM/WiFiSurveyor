using System.Collections.Generic;

namespace WiFiSurveyor
{
    public interface ISignalParser<in T>
    {
        IList<Signal> Parse(T results);
    }
}