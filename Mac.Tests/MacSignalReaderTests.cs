using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacSignalReaderTests
{
	[Fact]
	public async Task PoweredOffInterfaceReturnsNoNetworksWithoutRetry()
	{
		var calls = 0;
		var reader = new MacSignalReader(() => { calls++; return null; }, Substitute.For<IHostApplicationLifetime>());

		Assert.Empty(await reader.Read());
		Assert.Equal(1, calls);
	}

	[Fact]
	public async Task EmptyScanIsRetriedOnce()
	{
		var calls = 0;
		var reader = new MacSignalReader(() => ++calls == 1 ? [] : [new("network", "02:00:00:00:00:01", -60, 1, 1)], Substitute.For<IHostApplicationLifetime>());

		Assert.Equal("network", Assert.Single(await reader.Read()).Ssid);
		Assert.Equal(2, calls);
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task ShutdownCancelsRetryWithoutReportingAnErrorOrScanningAgain(bool alreadyStopping)
	{
		using var stopping = new CancellationTokenSource();
		if (alreadyStopping) stopping.Cancel();
		var lifetime = Substitute.For<IHostApplicationLifetime>();
		lifetime.ApplicationStopping.Returns(stopping.Token);
		var calls = 0;
		var reader = new MacSignalReader(() => { calls++; stopping.Cancel(); return []; }, lifetime);

		Assert.Empty(await reader.Read().WaitAsync(TimeSpan.FromSeconds(1)));
		Assert.Equal(alreadyStopping ? 0 : 1, calls);
	}

	[Fact]
	public async Task RedactedIdentifiersReportThePermissionProblem()
	{
		var reader = new MacSignalReader(() => [new(null, null, -60, 36, 2)], Substitute.For<IHostApplicationLifetime>());

		await Assert.ThrowsAsync<InvalidOperationException>(reader.Read);
	}

	[Fact]
	public async Task ShutdownDuringScanDoesNotReportAPermissionError()
	{
		using var stopping = new CancellationTokenSource();
		var lifetime = Substitute.For<IHostApplicationLifetime>();
		lifetime.ApplicationStopping.Returns(stopping.Token);
		var reader = new MacSignalReader(() => { stopping.Cancel(); return [new(null, null, -60, 36, 2)]; }, lifetime);

		Assert.Empty(await reader.Read());
	}
}
