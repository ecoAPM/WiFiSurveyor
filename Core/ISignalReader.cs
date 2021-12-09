namespace WiFiSurveyor.Core;

public interface ISignalReader<T>
{
	Task<T> Read();
}