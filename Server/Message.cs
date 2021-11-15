using System;
using System.Collections.Generic;

namespace WiFiSurveyor;

public class Message
{
	public string Status { get; set; }
	public IList<Signal> Signals { get; set; } = new List<Signal>();
	public DateTime LastUpdated { get; set; } = DateTime.Now;
}
