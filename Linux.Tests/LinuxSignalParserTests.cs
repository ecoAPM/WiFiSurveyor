using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Linux.Tests;

public sealed class LinuxSignalParserTests
{
	[Fact]
	public async Task ResultsAreParsedIntoSignals()
	{
		//arrange
		var contents = await File.ReadAllTextAsync("iwlist-output.txt");
		var signalParser = new LinuxSignalParser();

		//act
		var signals = signalParser.Parse(contents).ToList();

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