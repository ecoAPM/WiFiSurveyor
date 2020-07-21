using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WiFiHeatMap.Server
{
    public class SignalService<T> : BackgroundService
    {
        private readonly ISignalReader<T> _signalReader;
        private readonly ISignalParser<T> _signalParser;
        private readonly ISignalHub _signalHub;
        private readonly ILogger _logger;

        public SignalService(ISignalReader<T> reader, ISignalParser<T> parser, ISignalHub hub, ILoggerFactory logger)
        {
            _signalReader = reader;
            _signalParser = parser;
            _signalHub = hub;
            _logger = logger.CreateLogger("WiFiHeatMap");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogDebug(DateTime.Now + ": Reading...");
                    var results = await _signalReader.Read();
                    var signals = _signalParser.Parse(results);

                    var message = new Message { Signals = signals };
                    _logger.LogDebug(message.LastUpdated + ": " + JsonSerializer.Serialize(signals));
                    await _signalHub.SendMessage(message);
                }

                catch (Exception e)
                {
                    var message = new Message { Status = e.Message };
                    await _signalHub.SendMessage(message);
                    _logger.LogWarning(message.LastUpdated + ": " + message.Status);
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}