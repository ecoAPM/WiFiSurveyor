using System;
using System.Threading.Tasks;

namespace WiFiHeatMap.Server
{
    public class MacSignalReader : ISignalReader
    {
        public Task<string> Read()
        {
            throw new NotImplementedException("macOS is not currently supported");
        }
    }
}