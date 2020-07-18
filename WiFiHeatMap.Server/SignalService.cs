using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WiFiHeatMap.Server
{
    public class SignalService : BackgroundService
    {
        private readonly ISignalReader _signalReader;
        private readonly ISignalParser _signalParser;
        private readonly ISignalHub _signalHub;

        public SignalService(ISignalReader reader, ISignalParser parser, ISignalHub hub)
        {
            _signalReader = reader;
            _signalParser = parser;
            _signalHub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var results = await _signalReader.Read();
                    var signals = _signalParser.Parse(results);
                    
                    var message = new Message
                    {
                        Status = "Running",
                        Signals = signals,
                        LastUpdated = DateTime.Now
                    };
                    await _signalHub.SendMessage(message);
                    
                    await Task.Delay(1, stoppingToken);
                }
            }
            catch (Exception e)
            {
                var message = new Message
                {
                    Status = e.Message,
                    LastUpdated = DateTime.Now
                };
                await _signalHub.SendMessage(message);
            }
        }
    }
}