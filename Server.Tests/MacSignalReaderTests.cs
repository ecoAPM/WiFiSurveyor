using System.Diagnostics;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Tests;

public sealed class MacSignalReaderTests
{
	[Fact]
	public async Task ReturnsOutputFromProcess()
	{
		//arrange
		var commandService = Substitute.For<ICommandService>();
		commandService.Run(Arg.Any<ProcessStartInfo>()).Returns("file contents");
		var reader = new LinuxSignalReader(commandService);

		//act
		var results = await reader.Read();

		//assert
		Assert.Equal("file contents", results);
	}
}
