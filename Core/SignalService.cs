using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WiFiSurveyor.Core;

public sealed class SignalService<T> : BackgroundService
{
	private readonly ISignalReader<T> _signalReader;
	private readonly ISignalParser<T> _signalParser;
	private readonly ISignalHub _signalHub;
	private readonly ILogger _logger;

	private const ushort interval_ms = 1_000;

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
			try
			{
				if (_logger.IsEnabled(LogLevel.Debug))
					_logger.LogDebug("{time}: Receiving Wi-Fi signals...", DateTime.Now);
				var results = await _signalReader.Read();
				var signals = _signalParser.Parse(results);

				var message = new Message { Signals = signals };
				if (_logger.IsEnabled(LogLevel.Debug))
					_logger.LogDebug("{time}: {signalData}", message.LastUpdated, JsonSerializer.Serialize(signals));
				await _signalHub.SendMessage(message);
			}
			catch (Exception e)
			{
				var message = new Message { Status = e.Message };
				await _signalHub.SendMessage(message);
				if (_logger.IsEnabled(LogLevel.Error))
					_logger.LogError("{updated}: {status}", message.LastUpdated, message.Status);
			}

			await Task.Delay(interval_ms, stoppingToken);
		}
	}
}