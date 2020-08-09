using System;
using System.Threading.Tasks;

namespace WiFiHeatMap
{
    public class MacSignalReader : ISignalReader<string>
    {
        public Task<string> Read()
        {
            throw new NotImplementedException("macOS is not currently supported");
        }
    }
}