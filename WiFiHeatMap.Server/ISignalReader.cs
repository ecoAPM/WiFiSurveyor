using System.Threading.Tasks;

namespace WiFiHeatMap.Server
{
    public interface ISignalReader<T>
    {
        Task<T> Read();
    }
}