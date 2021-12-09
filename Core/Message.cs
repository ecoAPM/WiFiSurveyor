namespace WiFiSurveyor.Core;

public sealed class Message
{
	public string Status { get; init; } = string.Empty;
	public IReadOnlyList<Signal> Signals { get; init; } = new List<Signal>();
	public DateTime LastUpdated { get; init; } = DateTime.Now;
}