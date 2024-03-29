using System.ComponentModel;
using System.Diagnostics;
using NSubstitute;
using WiFiSurveyor.Core;
using Xunit;

namespace WiFiSurveyor.Linux.Tests;

public sealed class LinuxSignalReaderTests
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

	[Fact]
	public async Task ReturnsDecentMessageWhenNotFound()
	{
		//arrange
		var exception = new Win32Exception(2, "x");
		var commandService = Substitute.For<ICommandService>();
		commandService.When(c => c.Run(Arg.Any<ProcessStartInfo>())).Throw(exception);
		var reader = new LinuxSignalReader(commandService);

		try
		{
			//act
			await reader.Read();
		}
		catch (Exception e)
		{
			//assert
			Assert.Contains("\"wireless-tools\" is installed", e.Message);
			Assert.Contains("running as root", e.Message);
		}
	}

	[Fact]
	public async Task OtherExceptionsAreThrown()
	{
		//arrange
		var exception = new Win32Exception(1, "other error");
		var commandService = Substitute.For<ICommandService>();
		commandService.When(c => c.Run(Arg.Any<ProcessStartInfo>())).Throw(exception);
		var reader = new LinuxSignalReader(commandService);

		try
		{
			//act
			await reader.Read();
		}
		catch (Exception e)
		{
			//assert
			Assert.Contains("other error", e.Message);
		}
	}
}