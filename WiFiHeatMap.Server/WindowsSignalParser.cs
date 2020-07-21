using System;
using System.Collections.Generic;

namespace WiFiHeatMap.Server
{
    public class WindowsSignalParser : ISignalParser
    {
        public IList<Signal> Parse(string results)
        {
            throw new NotImplementedException("Windows is not currently supported");
        }
    }
}