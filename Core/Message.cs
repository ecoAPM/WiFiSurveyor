namespace WiFiSurveyor.Core;

public sealed class Message
{
	public string Status { get; init; } = string.Empty;
	public Signal[] Signals { get; init; } = [];
	public DateTime LastUpdated { get; } = DateTime.Now;
}