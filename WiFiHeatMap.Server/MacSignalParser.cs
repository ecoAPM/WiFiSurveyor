using System;
using System.Collections.Generic;

namespace WiFiHeatMap.Server
{
    public class MacSignalParser : ISignalParser
    {
        public IList<Signal> Parse(string results)
        {
            throw new NotImplementedException("macOS is not currently supported");
        }
    }
}