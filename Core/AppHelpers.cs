using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace WiFiSurveyor.Core;

public static class AppHelpers
{
	public static void AddCommonServices(this IServiceCollection services)
	{
		services.AddSingleton(s => s.GetService<ILoggerFactory>()!.CreateLogger("WiFiSurveyor"));
		services.AddSingleton<Func<ProcessStartInfo, Process?>>(Process.Start);
		services.AddSingleton<ISignalHub, SignalHub>();
		services.AddCors();
		services.AddLogging();
		services.AddResponseCompression();
		services.AddSignalR();
	}

	public static void AddMiddleware(this IApplicationBuilder app, IHostEnvironment env)
	{
		var port = env.IsProduction() ? 3000 : 5173;
		app.UseCors(builder => builder.AllowCredentials().AllowAnyHeader().WithOrigins($"http://localhost:{port}"));
		app.UseResponseCompression();
		app.UseDefaultFiles();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseEndpoints(builder => builder.MapHub<SignalHub>("/signals"));
	}

	public static void AddPosixHandlers(this IServiceCollection services)
	{
		services.AddSingleton<ICommandService, CommandService>();
		services.AddHostedService<SignalService<string>>();
	}

	public static async Task Run(this IHost app, IWebHostEnvironment env)
	{
		await app.StartAsync();

		if (!env.IsDevelopment())
		{
			app.LaunchBrowser();
		}

		await app.WaitForShutdownAsync();
	}

	private static void LaunchBrowser(this IHost app)
	{
		var address = app.Services.GetRequiredService<IServer>()
			.Features.Get<IServerAddressesFeature>()!
			.Addresses.First();

		var launcher = app.Services.GetRequiredService<IBrowserLauncher>();

		launcher.Run(address);
	}
}