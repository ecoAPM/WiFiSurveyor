namespace WiFiSurveyor.Core;

public class App
{
	private readonly WebApplication _app;

	public App(Action<IServiceCollection> addHandlers, string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddCommonServices();
		addHandlers(builder.Services);

		_app = builder.Build();
		_app.AddMiddleware();
	}

	public async Task Run()
	{
		_app.Start();
		_app.LaunchBrowser();
		await _app.WaitForShutdownAsync();
	}
}