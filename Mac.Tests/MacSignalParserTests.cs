using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacSignalParserTests
{
	[Fact]
	public void ScanResultsDoNotRequireAnAssociatedNetwork()
	{
		var parser = new MacSignalParser();
		var signals = parser.Parse("""
			{"Signals":[
				{"SSID":"ssid🏎","MAC":"02:00:00:00:00:01","Frequency":2,"Channel":9,"Strength":-39},
				{"SSID":"other-network","MAC":"02:00:00:00:00:02","Frequency":5,"Channel":44,"Strength":-50}
			]}
			""");

		Assert.Equal(2, signals.Count);
		Assert.Equal("ssid🏎", signals[0].SSID);
		Assert.Equal("02:00:00:00:00:01", signals[0].MAC);
		Assert.Equal(Frequency._2_4_GHz, signals[0].Frequency);
		Assert.Equal((byte)9, signals[0].Channel);
		Assert.Equal((short)-39, signals[0].Strength);
		Assert.Equal(Frequency._5_GHz, signals[1].Frequency);
		Assert.Equal((byte)44, signals[1].Channel);
	}

	[Fact]
	public void AnEmptyScanIsValid()
	{
		var parser = new MacSignalParser();
		Assert.Empty(parser.Parse("{\"Signals\":[]}"));
	}

	[Fact]
	public void ScannerErrorsAreReported()
	{
		var parser = new MacSignalParser();
		var error = Assert.Throws<System.InvalidOperationException>(() =>
			parser.Parse("{\"Error\":\"Wi-Fi is turned off.\"}"));
		Assert.Equal("Wi-Fi is turned off.", error.Message);
	}

	[Theory]
	[InlineData("{}")]
	[InlineData("{\"Signals\":null}")]
	[InlineData("{\"Signals\":{}}")]
	[InlineData("invalid JSON")]
	public void InvalidScannerOutputIsRejected(string output)
	{
		Assert.ThrowsAny<System.Text.Json.JsonException>(() => new MacSignalParser().Parse(output));
	}

	[Fact]
	public async Task ResultsAreParsedIntoSignals()
	{
		//arrange
		var signalParser = new MacSignalParser();
		var contents = await File.ReadAllTextAsync("scanner-output.json");

		//act
		var signals = signalParser.Parse(contents).ToList();

		//assert
		Assert.Equal("net1", signals[0].SSID);
		Assert.Equal("02:00:00:00:00:01", signals[0].MAC);
		Assert.Equal(Frequency._2_4_GHz, signals[0].Frequency);
		Assert.Equal(9, signals[0].Channel);
		Assert.Equal(-39, signals[0].Strength);

		Assert.Equal("ssid🏎2", signals[1].SSID);
		Assert.Equal("02:00:00:00:00:02", signals[1].MAC);
		Assert.Equal(Frequency._5_GHz, signals[1].Frequency);
		Assert.Equal(44, signals[1].Channel);
		Assert.Equal(-50, signals[1].Strength);

		Assert.Equal("access_point_3", signals[2].SSID);
		Assert.Equal("02:00:00:00:00:03", signals[2].MAC);
		Assert.Equal(Frequency._5_GHz, signals[2].Frequency);
		Assert.Equal(149, signals[2].Channel);
		Assert.Equal(-42, signals[2].Strength);

		Assert.Equal("wap-4", signals[3].SSID);
		Assert.Equal("02:00:00:00:00:04", signals[3].MAC);
		Assert.Equal(Frequency._2_4_GHz, signals[3].Frequency);
		Assert.Equal(11, signals[3].Channel);
		Assert.Equal(-90, signals[3].Strength);
	}
}
