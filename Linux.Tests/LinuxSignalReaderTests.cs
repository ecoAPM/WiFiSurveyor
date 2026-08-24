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
		var deviceLocator = Substitute.For<IDeviceLocator>();
		var reader = new LinuxSignalReader(commandService, deviceLocator);

		commandService.Run(Arg.Any<ProcessStartInfo>()).Returns("file contents");

		//act
		var results = await reader.Read();

		//assert
		Assert.Equal("file contents", results);
	}

	[Fact]
	public async Task ReturnsDecentMessageWhenNotFound()
	{
		//arrange
		var commandService = Substitute.For<ICommandService>();
		var deviceLocator = Substitute.For<IDeviceLocator>();
		var reader = new LinuxSignalReader(commandService, deviceLocator);

		var exception = new Win32Exception(2, "x");
		commandService.When(c => c.Run(Arg.Any<ProcessStartInfo>())).Throw(exception);

		try
		{
			//act
			await reader.Read();
		}
		catch (Exception e)
		{
			//assert
			Assert.Contains("\"iw\" is installed", e.Message);
			Assert.Contains("running as root", e.Message);
		}
	}

	[Fact]
	public async Task OtherExceptionsAreThrown()
	{
		//arrange
		var commandService = Substitute.For<ICommandService>();
		var deviceLocator = Substitute.For<IDeviceLocator>();
		var reader = new LinuxSignalReader(commandService, deviceLocator);

		var exception = new Win32Exception(1, "other error");
		commandService.When(c => c.Run(Arg.Any<ProcessStartInfo>())).Throw(exception);

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