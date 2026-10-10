using AppKit;
using CoreLocation;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using WiFiSurveyor.Core;

namespace WiFiSurveyor.Mac;

public static class Program
{
	public static void AddMacHandlers(this IServiceCollection services)
	{
		services.AddHostedService<SignalService<IReadOnlyList<IWiFiNetwork>>>();
		services.AddSingleton<IBrowserLauncher, MacBrowserLauncher>();
		services.AddSingleton<ISignalReader<IReadOnlyList<IWiFiNetwork>>, MacSignalReader>();
		services.AddSingleton<ISignalParser<IReadOnlyList<IWiFiNetwork>>, MacSignalParser>();
	}

	public static void Main(string[] args)
	{
		using var startupPool = new NSAutoreleasePool();
		NSApplication.Init();
		using var location = new CLLocationManager();
		location.RequestWhenInUseAuthorization();

		var server = Task.Run(() =>
		{
			using var pool = new NSAutoreleasePool();
			return new App(services =>
			{
				services.AddMacHandlers();
				services.AddSingleton(location);
			}, args, NSBundle.MainBundle.ResourcePath).Run();
		});

		// CoreLocation delivers authorization changes on the creating thread's run loop.
		while (!server.IsCompleted)
		{
			using var pool = new NSAutoreleasePool();
			NSRunLoop.Main.RunUntil(NSDate.FromTimeIntervalSinceNow(1));
		}
		server.GetAwaiter().GetResult();
	}
}
