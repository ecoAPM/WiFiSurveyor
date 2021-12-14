namespace WiFiSurveyor.Core;

public sealed class App
{
	private readonly IHost _app;
	private readonly IWebHostEnvironment _env;

	public App(Action<IServiceCollection> addHandlers, string[] args)
	{
		var options = new WebApplicationOptions
		{
			Args = args,
			EnvironmentName = args.All(a => a != "dev")
				? Environments.Production
				: Environments.Development,
			WebRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "_content", "WiFiSurveyor.Core")
		};

		var builder = WebApplication.CreateBuilder(options);
		_env = builder.Environment;

		if (!_env.IsDevelopment())
		{
			builder.WebHost.UseUrls("http://127.0.0.1:0");
		}

		builder.Services.AddCommonServices();
		addHandlers(builder.Services);

		var app = builder.Build();
		app.AddMiddleware();

		_app = app;
	}

	public async Task Run()
		=> await _app.Run(_env);
}