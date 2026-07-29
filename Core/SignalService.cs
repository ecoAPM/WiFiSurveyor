using System.Text.Json;

namespace WiFiSurveyor.Core;

public sealed class SignalService<T>(ISignalReader<T> reader, ISignalParser<T> parser, ISignalHub hub, ILogger logger) : BackgroundService
{
	private const ushort interval_ms = 1_000;

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			await GetSignals();
			await Task.Delay(interval_ms, stoppingToken);
		}
	}

	public async Task GetSignals()
	{
		try
		{
			logger.LogIf(LogLevel.Debug, "{time}: Receiving Wi-Fi signals...", DateTime.Now);
			var results = await reader.Read();
			var signals = parser.Parse(results);

			var message = new Message { Signals = signals };
			logger.LogIf(LogLevel.Debug, "{time}: {signalData}", message.LastUpdated, JsonSerializer.Serialize(signals));

			await hub.SendMessage(message);
		}
		catch (Exception e)
		{
			var message = new Message { Status = e.Message };
			await hub.SendMessage(message);
			logger.LogIf(LogLevel.Error, "{updated}: {status}", message.LastUpdated, message.Status);
			logger.LogIf(LogLevel.Debug, "{exception}", e.ToString());
		}
	}
}