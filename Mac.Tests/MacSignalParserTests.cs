using System;
using CoreWlan;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacSignalParserTests
{
	[Theory]
	[InlineData(CWChannelBand.TwoGHz, 9, Frequency._2_4_GHz)]
	[InlineData(CWChannelBand.FiveGHz, 44, Frequency._5_GHz)]
	public void ResultsAreParsedIntoSignals(CWChannelBand band, int channel, Frequency frequency)
	{
		var network = Network("ssid🏎", "02:00:00:00:00:01", band, channel, -39);
		var signal = Assert.Single(new MacSignalParser(Substitute.For<ILogger>()).Parse([network]));

		Assert.Equal("ssid🏎", signal.SSID);
		Assert.Equal("02:00:00:00:00:01", signal.MAC);
		Assert.Equal(frequency, signal.Frequency);
		Assert.Equal(channel, signal.Channel);
		Assert.Equal(-39, signal.Strength);
	}

	[Fact]
	public void HiddenNetworkKeepsItsBssid()
	{
		var network = Network(null, "02:00:00:00:00:01", CWChannelBand.TwoGHz, 9, -39);
		var signal = Assert.Single(new MacSignalParser(Substitute.For<ILogger>()).Parse([network]));
		Assert.Empty(signal.SSID);
		Assert.Equal("02:00:00:00:00:01", signal.MAC);
	}

	[Fact]
	public void UnsupportedOrInvalidNetworksDoNotDiscardValidNetworks()
	{
		var good = Network("good", "02:00:00:00:00:01", CWChannelBand.TwoGHz, 9, -39);
		var unsupported = Network("other", "02:00:00:00:00:02", CWChannelBand.Unknown, 1, -40);
		var withoutBssid = Network("other", null, CWChannelBand.FiveGHz, 44, -40);
		var invalid = Network("other", "02:00:00:00:00:03", CWChannelBand.FiveGHz, 256, -40);
		var signal = Assert.Single(new MacSignalParser(Substitute.For<ILogger>()).Parse([unsupported, withoutBssid, invalid, good]));
		Assert.Equal("good", signal.SSID);
	}

	[Fact]
	public void RepeatedScanResultsDoNotDuplicateHiddenNetworks()
	{
		var first = Network(null, "02:00:00:00:00:01", CWChannelBand.FiveGHz, 44, -39);
		var repeated = Network(null, "02:00:00:00:00:01", CWChannelBand.FiveGHz, 44, -39);
		var other = Network(null, "02:00:00:00:00:02", CWChannelBand.FiveGHz, 44, -40);
		var signals = new MacSignalParser(Substitute.For<ILogger>()).Parse([first, repeated, other]);

		Assert.Collection(signals,
			signal => Assert.Equal("02:00:00:00:00:01", signal.MAC),
			signal => Assert.Equal("02:00:00:00:00:02", signal.MAC));
	}

	private static IWiFiNetwork Network(string? ssid, string? bssid, CWChannelBand band, int channel, int rssi)
	{
		var network = Substitute.For<IWiFiNetwork>();
		network.Ssid.Returns(ssid);
		network.Bssid.Returns(bssid);
		network.Band.Returns(band);
		network.Channel.Returns((nint)channel);
		network.Rssi.Returns((nint)rssi);
		return network;
	}
}
