using System.Threading.Tasks;

namespace WiFiHeatMap.Server
{
    public interface ISignalReader
    {
        Task<string> Read();
    }
}