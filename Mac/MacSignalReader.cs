using Microsoft.Extensions.Hosting;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalReader(Func<IReadOnlyList<WiFiNetwork>?> scan, IHostApplicationLifetime lifetime) : ISignalReader<IReadOnlyList<WiFiNetwork>>
{
	public async Task<IReadOnlyList<WiFiNetwork>> Read()
	{
		var stoppingToken = lifetime.ApplicationStopping;
		try
		{
			stoppingToken.ThrowIfCancellationRequested();
			var networks = scan();
			// macOS can briefly return no networks while Wi-Fi remains enabled.
			if (networks is { Count: 0 })
			{
				await Task.Delay(5_000, stoppingToken);
				stoppingToken.ThrowIfCancellationRequested();
				networks = scan();
			}
			stoppingToken.ThrowIfCancellationRequested();
			if (networks is { Count: > 0 } && networks.All(network => string.IsNullOrEmpty(network.Bssid)))
			{
				throw new InvalidOperationException("macOS has hidden Wi-Fi identifiers. Reading them requires Location Services permission.");
			}
			return networks ?? [];
		}
		catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
		{
			return [];
		}
	}
}
