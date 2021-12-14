using NSubstitute;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Windows.Tests;

public sealed class WindowsSignalParserTests
{
	[Fact]
	public void ResultsAreParsedIntoSignals()
	{
		//arrange
		var network1 = Substitute.For<IWiFiAvailableNetwork>();
		network1.Ssid.Returns("Net1");
		network1.ChannelCenterFrequencyInKilohertz.Returns(2_400_000);
		network1.NetworkRssiInDecibelMilliwatts.Returns(-65);

		var network2 = Substitute.For<IWiFiAvailableNetwork>();
		network2.Ssid.Returns("Net1");
		network2.ChannelCenterFrequencyInKilohertz.Returns(5_000_000);
		network2.NetworkRssiInDecibelMilliwatts.Returns(-83);

		var network3 = Substitute.For<IWiFiAvailableNetwork>();
		network3.Ssid.Returns("Net2");
		network3.ChannelCenterFrequencyInKilohertz.Returns(2_400_000);
		network3.NetworkRssiInDecibelMilliwatts.Returns(-72);

		var network4 = Substitute.For<IWiFiAvailableNetwork>();
		network4.Ssid.Returns("Net3");
		network4.ChannelCenterFrequencyInKilohertz.Returns(2_400_000);
		network4.NetworkRssiInDecibelMilliwatts.Returns(-90);

		var networks = new[]
		{
			network1,
			network2,
			network3,
			network4
		};

		var report = Substitute.For<IWiFiNetworkReport>();
		report.AvailableNetworks().Returns(networks);

		var signalParser = new WindowsSignalParser();

		//act
		var signals = signalParser.Parse(report).ToList();

		//assert
		Assert.Equal("Net1", signals[0].SSID);
		Assert.Equal(Frequency._2_4_GHz, signals[0].Frequency);
		Assert.Equal(-65, signals[0].Strength);

		Assert.Equal("Net1", signals[1].SSID);
		Assert.Equal(Frequency._5_GHz, signals[1].Frequency);
		Assert.Equal(-83, signals[1].Strength);

		Assert.Equal("Net2", signals[2].SSID);
		Assert.Equal(Frequency._2_4_GHz, signals[2].Frequency);
		Assert.Equal(-72, signals[2].Strength);

		Assert.Equal("Net3", signals[3].SSID);
		Assert.Equal(Frequency._2_4_GHz, signals[3].Frequency);
		Assert.Equal(-90, signals[3].Strength);
	}
}