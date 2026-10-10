using CoreLocation;
using CoreWlan;
using Foundation;
using Microsoft.Extensions.Hosting;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public sealed class MacSignalReader(CLLocationManager location, IHostApplicationLifetime lifetime) : ISignalReader<IReadOnlyList<IWiFiNetwork>>
{
	public Task<IReadOnlyList<IWiFiNetwork>> Read()
		=> Read(Scan, lifetime.ApplicationStopping);

	internal static async Task<IReadOnlyList<IWiFiNetwork>> Read(Func<IReadOnlyList<IWiFiNetwork>?> scan, CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();
		var networks = scan();
		// macOS can briefly return an empty cache while reassociating.
		if (networks is { Count: 0 })
		{
			await Task.Delay(5_000, stoppingToken);
			stoppingToken.ThrowIfCancellationRequested();
			networks = scan();
		}
		return networks ?? [];
	}

	private IReadOnlyList<IWiFiNetwork>? Scan()
	{
		using var pool = new NSAutoreleasePool();
		var wifi = CWWiFiClient.SharedWiFiClient.MainInterface;
		if (wifi is null || !wifi.PowerOn)
		{
			return null;
		}

		if (!CLLocationManager.LocationServicesEnabled || location.AuthorizationStatus != CLAuthorizationStatus.Authorized)
		{
			throw new InvalidOperationException("Allow WiFiSurveyor in System Settings > Privacy & Security > Location Services to read Wi-Fi network names.");
		}

		var networks = wifi.ScanForNetworksWithSsid(null!, false, out var error);
		// Cocoa reports failure through a null result, not the NSError output alone.
		if (networks is null)
		{
			throw new InvalidOperationException(error?.LocalizedDescription ?? "macOS did not return Wi-Fi scan results.");
		}

		if (networks is { Length: > 0 } && networks.All(network => string.IsNullOrEmpty(network.Bssid)))
		{
			throw new InvalidOperationException("macOS has hidden Wi-Fi identifiers. Launch WiFiSurveyor.app from Finder and allow Location Services.");
		}

		return networks.Select(network => (IWiFiNetwork)new WiFiNetwork(network)).ToArray();
	}
}
