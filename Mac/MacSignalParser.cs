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
			.GetProperty("SPAirPortDataType")
			.EnumerateArray()
			.First()
			.GetProperty("spairport_airport_interfaces")
			.EnumerateArray()
			.First()
			.GetProperty("spairport_airport_other_local_wireless_networks")
			.EnumerateArray()
			.Select(e => GetSignal(e))
			.Where(s => s is not null)
			.Cast<Signal>()
			.ToArray();

	private Signal? GetSignal(JsonElement e)
	{
		try
		{
			return new Signal
			{
				SSID = e.GetProperty("_name").GetString()!,
				MAC = string.Empty,
				Strength = GetStrength(e.GetProperty("spairport_signal_noise").GetString()!),
				Channel = GetChannel(e.GetProperty("spairport_network_channel").GetString()!),
				Frequency = GetFrequency(e.GetProperty("spairport_network_channel").GetString()!)
			};
		}
		catch (Exception)
		{
			_logger.LogIf(LogLevel.Warning, "Could not parse signal data: {0}", e.ToString());
			return null;
		}
	}

	private static short GetStrength(string value)
		=> short.Parse(value.Split(" ")[0]);

	private static Frequency GetFrequency(string column)
		=> GetChannel(column) < 32
			? Frequency._2_4_GHz
			: Frequency._5_GHz;

	private static byte GetChannel(string column)
		=> byte.Parse(column.Split(',')[0]);
}