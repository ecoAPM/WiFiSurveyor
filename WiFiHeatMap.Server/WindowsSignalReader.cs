using System;
using System.Threading.Tasks;

namespace WiFiHeatMap.Server
{
    public class WindowsSignalReader : ISignalReader
    {
        public Task<string> Read()
        {
            throw new NotImplementedException("Windows is not currently supported");
        }
    }
}