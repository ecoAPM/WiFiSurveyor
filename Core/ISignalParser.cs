namespace WiFiSurveyor.Core;

public interface ISignalParser<in T>
{
	Signal[] Parse(T results);
}