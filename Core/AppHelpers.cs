namespace WiFiSurveyor.Core;

public static class AppHelpers
{
	public static void AddCommonServices(this IServiceCollection services)
	{
		services.AddWiFiSurveyor();
		services.AddCors();
		services.AddLogging();
		services.AddResponseCompression();
		services.AddSignalR();
	}

	public static void AddMiddleware(this IApplicationBuilder app)
	{
		app.UseCors(builder => builder.AllowCredentials().AllowAnyHeader().WithOrigins("http://localhost:3000"));
		app.UseResponseCompression();
		app.UseDefaultFiles();
		app.UseStaticFiles();
		app.UseRouting();
		app.UseEndpoints(builder => builder.MapHub<SignalHub>("/signals"));
	}

	public static void LaunchBrowser(this WebApplication app)
	{
		var address = app.Urls.First();

		var browser = app.Services.GetService<BrowserLauncher>()
		              ?? throw new NullReferenceException("Could not find browser launcher");

		browser.Run(address);
	}
}