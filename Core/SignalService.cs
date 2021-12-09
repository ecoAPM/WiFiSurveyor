using System.Text.Json;

namespace WiFiSurveyor.Core;

public sealed class SignalService<T> : BackgroundService
{
	private const ushort interval_ms = 1_000;
	private readonly ILogger _logger;
	private readonly ISignalHub _signalHub;
	private readonly ISignalParser<T> _signalParser;
	private readonly ISignalReader<T> _signalReader;

	public SignalService(ISignalReader<T> reader, ISignalParser<T> parser, ISignalHub hub, ILogger logger)
	{
		_signalReader = reader;
		_signalParser = parser;
		_signalHub = hub;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			await GetSignals();
			await Task.Delay(interval_ms, stoppingToken);
		}
	}

	private async Task GetSignals()
	{
		try
		{
			_logger.LogIf(LogLevel.Debug, "{time}: Receiving Wi-Fi signals...", DateTime.Now);
			var results = await _signalReader.Read();
			var signals = _signalParser.Parse(results);

			var message = new Message { Signals = signals };
			_logger.LogIf(LogLevel.Debug, "{time}: {signalData}", message.LastUpdated, JsonSerializer.Serialize(signals));

			await _signalHub.SendMessage(message);
		}
		catch (Exception e)
		{
			var message = new Message { Status = e.Message };
			await _signalHub.SendMessage(message);
			_logger.LogIf(LogLevel.Error, "{updated}: {status}", message.LastUpdated, message.Status);
		}
	}
}