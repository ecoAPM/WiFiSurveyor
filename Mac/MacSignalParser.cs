using System.Text.Json;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalParser : ISignalParser<string>
{
	public IReadOnlyList<Signal> Parse(string results)
	{
		using var json = JsonDocument.Parse(results);
		if (json.RootElement.TryGetProperty("Error", out var error))
			throw new InvalidOperationException(error.GetString());

		if (!json.RootElement.TryGetProperty("Signals", out var signals)
			|| signals.ValueKind != JsonValueKind.Array)
			throw new JsonException("The Wi-Fi scanner did not return signal data.");
		return signals.Deserialize<Signal[]>()!;
	}
}
