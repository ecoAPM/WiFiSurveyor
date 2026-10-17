using System;
using System.Threading.Tasks;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacApplicationTests
{
	[Fact]
	public void PumpsUntilTheServerStops()
	{
		var server = new TaskCompletionSource();
		var calls = 0;

		MacApplication.WaitForServer(server.Task, () => { calls++; server.SetResult(); });

		Assert.Equal(1, calls);
	}

	[Fact]
	public void CompletedServerDoesNotPump()
		=> MacApplication.WaitForServer(Task.CompletedTask, () => throw new Exception("Unexpected run loop call"));

	[Fact]
	public void ServerFailuresArePropagated()
	{
		var error = new InvalidOperationException("Server failed");

		Assert.Same(error, Assert.Throws<InvalidOperationException>(() =>
			MacApplication.WaitForServer(Task.FromException(error), () => { })));
	}
}
