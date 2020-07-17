using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WiFiHeatMap
{
    public class SignalService : BackgroundService, ISignalService
    {
        private readonly ISignalReader _signalReader;
        private readonly ISignalParser _signalParser;

        public IEnumerable<Signal> Signals { get; private set; }
        public string Status { get; private set; }

        public SignalService(ISignalReader signalReader, ISignalParser signalParser)
        {
            _signalReader = signalReader;
            _signalParser = signalParser;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var results = await _signalReader.Read();
                    Signals = _signalParser.Parse(results);
                    await Task.Delay(1, stoppingToken);
                }
            }
            catch (Exception e)
            {
                Status = e.Message;
            }
        }
    }
}