using System.Collections.Generic;

namespace WiFiSurveyor.Core;

public interface ISignalParser<in T>
{
	IList<Signal> Parse(T results);
}