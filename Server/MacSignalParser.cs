using System;
using System.Collections.Generic;

namespace WiFiSurveyor;

public class MacSignalParser : ISignalParser<string>
{
	public IList<Signal> Parse(string results)
	{
		throw new NotImplementedException("macOS is not currently supported");
	}
}
