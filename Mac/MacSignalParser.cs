using System.Text;
using System.Text.Json;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalParser : ISignalParser<string>
{
	private readonly ILogger _logger;

	public MacSignalParser(ILogger logger)
		=> _logger = logger;

	public IReadOnlyList<Signal> Parse(string results)
		=> JsonSerializer.Deserialize<JsonElement>(results)
			.GetProperty("SPAirPortDataType").EnumerateArray().First()
			.GetProperty("spairport_airport_interfaces").EnumerateArray().First()
			.GetProperty("spairport_airport_other_local_wireless_networks").EnumerateArray()
			.Select(j => GetSignal(j))
			.Where(s => s is not null)
			.Cast<Signal>()
			.ToArray();

	private Signal? GetSignal(JsonElement json)
	{
		try
		{
			return new Signal
			{
				SSID = GetString(json, "_name"),
				MAC = string.Empty,
				Strength = GetStrength(GetString(json, "spairport_signal_noise")),
				Channel = GetChannel(GetString(json, "spairport_network_channel")),
				Frequency = GetFrequency(GetString(json, "spairport_network_channel"))
			};
		}
		catch (Exception e)
		{
			_logger.LogIf(LogLevel.Warning, "{now}: Could not parse signal data -- {data}", DateTime.Now, json.ToString());
			_logger.LogIf(LogLevel.Debug, "{exception}", e.ToString());
			return null;
		}
	}

	private static string GetString(JsonElement json, string property)
		=> json.GetProperty(property).GetString() ?? string.Empty;

	private static short GetStrength(string value)
		=> short.Parse(value.Split(' ')[0]);

	private static Frequency GetFrequency(string value)
		=> GetChannel(value) < 32
			? Frequency._2_4_GHz
			: Frequency._5_GHz;

	private static byte GetChannel(string value)
		=> byte.Parse(value.Split(' ')[0]);
}