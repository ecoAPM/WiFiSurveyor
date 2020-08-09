using System.Threading.Tasks;

namespace WiFiHeatMap
{
    public interface ISignalReader<T>
    {
        Task<T> Read();
    }
}