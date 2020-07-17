using System.Threading.Tasks;

namespace WiFiHeatMap
{
    public interface ISignalReader
    {
        Task<string> Read();
    }
}