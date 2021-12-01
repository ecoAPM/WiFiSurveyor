using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Tests;

public sealed class CommandServiceTests
{
	[Fact]
	public async Task CanReadFromStdOut()
	{
		//arrange
		var logger = Substitute.For<ILogger>();
		var service = new CommandService(Process.Start, logger);
		var info = new ProcessStartInfo("ls");

		//act
		var output = await service.Run(info);

		//assert
		Assert.Contains("WiFiSurveyor.Tests.dll", output);
	}

	[Fact]
	public async Task FailureToLaunchIsHandled()
	{
		//arrange
		Process startProcess(ProcessStartInfo i) => null;
		var logger = Substitute.For<ILogger>();
		var service = new CommandService(startProcess, logger);
		var info = new ProcessStartInfo("not installed");

		//act
		var task = service.Run(info);
		await task;

		//assert
		Assert.True(task.IsCompletedSuccessfully);
	}

	[Fact]
	public async Task HangingProcessIsHandled()
	{
		//arrange
		var logger = Substitute.For<ILogger>();
		var service = new CommandService(Process.Start, logger, TimeSpan.Zero);
		var info = new ProcessStartInfo("sleep", "1");

		//act
		var task = service.Run(info);
		await task;

		//assert
		Assert.True(task.IsCompletedSuccessfully);
	}
}