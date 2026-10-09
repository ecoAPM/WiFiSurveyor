using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacSignalReaderTests
{
	[Fact]
	public async Task LaunchesTheScannerAsAnApplication()
	{
		var commandService = Substitute.For<ICommandService>();
		string? outputFile = null;
		commandService.Run(Arg.Any<ProcessStartInfo>()).Returns(call =>
		{
			var info = call.Arg<ProcessStartInfo>()!;
			Assert.Equal("/usr/bin/open", info.FileName);
			Assert.Equal(new[] { "-n", "-W", "-g", "--stdout" }, info.ArgumentList.Take(4));
			outputFile = info.ArgumentList[4];
			Assert.True(File.Exists(outputFile));
			Assert.Equal(Path.Combine(AppContext.BaseDirectory, "WiFiSurveyor.Scanner.app"), info.ArgumentList[5]);
			File.WriteAllText(outputFile, "{\"Signals\":[]}");
			return string.Empty;
		});

		Assert.Equal("{\"Signals\":[]}", await new MacSignalReader(commandService).Read());
		Assert.False(File.Exists(outputFile));
	}

	[Fact]
	public async Task DeletesOutputIfLaunchFails()
	{
		var commandService = Substitute.For<ICommandService>();
		string? outputFile = null;
		commandService.Run(Arg.Any<ProcessStartInfo>()).Returns<string>(call =>
		{
			outputFile = call.Arg<ProcessStartInfo>()!.ArgumentList[4];
			throw new InvalidOperationException("Launch failed.");
		});

		await Assert.ThrowsAsync<InvalidOperationException>(() => new MacSignalReader(commandService).Read());
		Assert.False(File.Exists(outputFile));
	}

	[Fact]
	public async Task MissingScannerOutputIsReported()
	{
		var commandService = Substitute.For<ICommandService>();
		commandService.Run(Arg.Any<ProcessStartInfo>()).Returns(string.Empty);
		var error = await Assert.ThrowsAsync<InvalidOperationException>(() => new MacSignalReader(commandService).Read());
		Assert.Equal("The Wi-Fi scanner did not return data.", error.Message);
	}
}
