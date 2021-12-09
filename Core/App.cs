namespace WiFiSurveyor.Core;

public sealed class App
{
	private readonly WebApplication _app;

	public App(Action<IServiceCollection> addHandlers, string[] args)
	{
		var options = new WebApplicationOptions
		{
			Args = args,
			WebRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "_content", "WiFiSurveyor.Core")
		};
		var builder = WebApplication.CreateBuilder(options);
		builder.WebHost.UseUrls("http://127.0.0.1:0");
		builder.Services.AddCommonServices();
		addHandlers(builder.Services);

		_app = builder.Build();
		_app.AddMiddleware();
	}

	public async Task Run()
	{
		await _app.StartAsync();
		_app.LaunchBrowser();
		await _app.WaitForShutdownAsync();
	}
}