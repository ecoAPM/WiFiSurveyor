namespace WiFiSurveyor.Core;

public sealed class App
{
	private readonly IHost _app;
	public IWebHostEnvironment Environment { get; }

	private const string BaseURL = "http://127.0.0.1:0";

	public App(Action<IServiceCollection> addHandlers, string[] args, string? contentRootPath = null)
	{
		var options = new WebApplicationOptions
		{
			Args = args,
			EnvironmentName = args.All(a => a != "dev")
				? Environments.Production
				: Environments.Development,
			ContentRootPath = contentRootPath,
			WebRootPath = Path.Combine(contentRootPath ?? AppContext.BaseDirectory, "wwwroot")
		};

		var builder = WebApplication.CreateBuilder(options);
		Environment = builder.Environment;

		if (Environment.IsProduction())
		{
			builder.WebHost.UseUrls(BaseURL);
		}

		builder.Services.AddCommonServices();
		addHandlers(builder.Services);

		var app = builder.Build();
		app.AddMiddleware(Environment);

		_app = app;
	}

	public async Task Run()
		=> await _app.Run(Environment);
}