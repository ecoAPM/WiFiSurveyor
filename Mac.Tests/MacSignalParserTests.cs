using Microsoft.Extensions.Logging;
using NSubstitute;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacSignalParserTests
{
	[Theory]
	[InlineData("ssid🏎", "02:00:00:00:00:01", -39, 9, 1, Frequency._2_4_GHz)]
	[InlineData(null, "02:00:00:00:00:02", -50, 44, 2, Frequency._5_GHz)]
	public void ResultsAreParsedIntoSignals(string? ssid, string? bssid, long rssi, long channel, long band, Frequency frequency)
	{
		var parser = new MacSignalParser(Substitute.For<ILogger>());
		var signal = Assert.Single(parser.Parse([new WiFiNetwork(ssid, bssid, rssi, channel, band)]));

		Assert.Equal(ssid ?? string.Empty, signal.SSID);
		Assert.Equal(bssid ?? string.Empty, signal.MAC);
		Assert.Equal(rssi, signal.Strength);
		Assert.Equal(channel, signal.Channel);
		Assert.Equal(frequency, signal.Frequency);
	}

	[Fact]
	public void UnsupportedBandsAndInvalidMeasurementsDoNotDiscardValidNetworks()
	{
		var parser = new MacSignalParser(Substitute.For<ILogger>());
		var signals = parser.Parse([
			new("unsupported", "02:00:00:00:00:01", -50, 5, 3),
			new("redacted", null, -50, 1, 1),
			new("invalid", "02:00:00:00:00:02", long.MinValue, 1, 1),
			new("valid", "02:00:00:00:00:03", -60, 36, 2)
		]);

		Assert.Equal("valid", Assert.Single(signals).SSID);
	}

	[Fact]
	public void DuplicateResultsAreRemovedWithoutMergingDifferentAccessPoints()
	{
		var parser = new MacSignalParser(Substitute.For<ILogger>());
		var first = new WiFiNetwork(null, "02:00:00:00:00:01", -50, 1, 1);
		var second = first with { Bssid = "02:00:00:00:00:02" };

		Assert.Equal(2, parser.Parse([first, first, second]).Count);
	}

}
