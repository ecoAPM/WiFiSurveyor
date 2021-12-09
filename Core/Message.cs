namespace WiFiSurveyor.Core;

public sealed class Message
{
	public string Status { get; set; } = string.Empty;
	public IList<Signal> Signals { get; set; } = new List<Signal>();
	public DateTime LastUpdated { get; set; } = DateTime.Now;
}