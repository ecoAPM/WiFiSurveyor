using System.Threading.Tasks;

namespace WiFiSurveyor;

public interface ISignalReader<T>
{
	Task<T> Read();
}