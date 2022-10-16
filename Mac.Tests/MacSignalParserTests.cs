using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacSignalParserTests
{
	[Fact]
	public async Task ResultsAreParsedIntoSignals()
	{
		//arrange
		var contents = await File.ReadAllTextAsync("airport-output.txt");
		var signalParser = new MacSignalParser();

		//act
		var signals = signalParser.Parse(contents).ToList();

		//assert
		Assert.Equal("net1", signals[0].SSID);
		Assert.Equal("xx:bf:xx:f7:13:xx", signals[0].MAC);
		Assert.Equal(Frequency._2_4_GHz, signals[0].Frequency);
		Assert.Equal(11, signals[0].Channel);
		Assert.Equal(-83, signals[0].Strength);

		Assert.Equal("ssid2", signals[1].SSID);
		Assert.Equal("xx:3d:xx:96:97:xx", signals[1].MAC);
		Assert.Equal(Frequency._5_GHz, signals[1].Frequency);
		Assert.Equal(149, signals[1].Channel);
		Assert.Equal(-82, signals[1].Strength);

		Assert.Equal("access_point_3", signals[2].SSID);
		Assert.Equal("xx:3d:xx:96:97:xx", signals[2].MAC);
		Assert.Equal(Frequency._2_4_GHz, signals[2].Frequency);
		Assert.Equal(11, signals[2].Channel);
		Assert.Equal(-76, signals[2].Strength);

		Assert.Equal("wap-4", signals[3].SSID);
		Assert.Equal("xx:cb:xx:ad:a8:xx", signals[3].MAC);
		Assert.Equal(Frequency._2_4_GHz, signals[3].Frequency);
		Assert.Equal(6, signals[3].Channel);
		Assert.Equal(-37, signals[3].Strength);

		Assert.Equal("router5", signals[4].SSID);
		Assert.Equal("xx:cb:xx:ad:a8:xx", signals[4].MAC);
		Assert.Equal(Frequency._5_GHz, signals[4].Frequency);
		Assert.Equal(149, signals[4].Channel);
		Assert.Equal(-42, signals[4].Strength);
	}
}