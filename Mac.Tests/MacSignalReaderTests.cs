using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace WiFiSurveyor.Mac.Tests;

public sealed class MacSignalReaderTests
{
	[Fact]
	public async Task NonemptyScanDoesNotRetry()
	{
		IReadOnlyList<IWiFiNetwork> networks = [Substitute.For<IWiFiNetwork>()];
		var calls = 0;

		var result = await MacSignalReader.Read(() => { calls++; return networks; }, CancellationToken.None);

		Assert.Same(networks, result);
		Assert.Equal(1, calls);
	}

	[Fact]
	public async Task EmptyScanIsConfirmedBeforeReturningNoNetworks()
	{
		IReadOnlyList<IWiFiNetwork> networks = [Substitute.For<IWiFiNetwork>()];
		var calls = 0;

		var result = await MacSignalReader.Read(() => ++calls == 1 ? [] : networks, CancellationToken.None);

		Assert.Same(networks, result);
		Assert.Equal(2, calls);
	}

	[Fact]
	public async Task PersistentEmptyScanReturnsNoNetworks()
	{
		var calls = 0;

		var result = await MacSignalReader.Read(() => { calls++; return []; }, CancellationToken.None);

		Assert.Empty(result);
		Assert.Equal(2, calls);
	}

	[Fact]
	public async Task PoweredOffInterfaceDoesNotRetry()
	{
		var calls = 0;

		var result = await MacSignalReader.Read(() => { calls++; return null; }, CancellationToken.None);

		Assert.Empty(result);
		Assert.Equal(1, calls);
	}

	[Fact]
	public async Task ScanFailureIsNotRetried()
	{
		var calls = 0;
		await Assert.ThrowsAsync<IOException>(() => MacSignalReader.Read(() => { calls++; throw new IOException(); }, CancellationToken.None));
		Assert.Equal(1, calls);
	}

	[Fact]
	public async Task ShutdownCancelsEmptyScanRetry()
	{
		using var stopping = new CancellationTokenSource();
		var calls = 0;
		var read = MacSignalReader.Read(() => { calls++; return []; }, stopping.Token);

		stopping.Cancel();

		await Assert.ThrowsAnyAsync<OperationCanceledException>(() => read);
		Assert.Equal(1, calls);
	}

	[Fact]
	public async Task ShutdownPreventsStartingAScan()
	{
		var calls = 0;

		await Assert.ThrowsAnyAsync<OperationCanceledException>(() => MacSignalReader.Read(() => { calls++; return []; }, new CancellationToken(true)));

		Assert.Equal(0, calls);
	}
}
