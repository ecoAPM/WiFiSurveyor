using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WiFiHeatMap.Server
{
    public class SignalService : BackgroundService
    {
        private readonly ISignalReader _signalReader;
        private readonly ISignalParser _signalParser;
        private readonly ISignalHub _signalHub;
        private readonly ILogger _logger;

        public SignalService(ISignalReader reader, ISignalParser parser, ISignalHub hub, ILoggerFactory logger)
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
                    _logger.LogInformation(DateTime.Now + ": Reading...");
                    var results = await _signalReader.Read();
                    var signals = _signalParser.Parse(results);

                    var message = new Message
                    {
                        Status = "Running",
                        Signals = signals,
                        LastUpdated = DateTime.Now
                    };
                    _logger.LogInformation(message.LastUpdated + ": " + message.Status);
                    await _signalHub.SendMessage(message);
                }

                catch (Exception e)
                {
                    var message = new Message
                    {
                        Status = e.Message,
                        LastUpdated = DateTime.Now
                    };
                    await _signalHub.SendMessage(message);
                    _logger.LogError(message.LastUpdated + ": " + message.Status);
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}