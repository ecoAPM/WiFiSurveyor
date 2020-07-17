using System.Collections.Generic;
using Microsoft.Extensions.Hosting;

namespace WiFiHeatMap
{
    public interface ISignalService : IHostedService
    {
        IEnumerable<Signal> Signals { get; }
    }
}