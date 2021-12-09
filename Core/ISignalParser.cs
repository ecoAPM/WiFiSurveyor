namespace WiFiSurveyor.Core;

public interface ISignalParser<in T>
{
	IReadOnlyList<Signal> Parse(T results);
}