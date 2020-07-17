using System;
using System.Collections.Generic;

namespace WiFiHeatMap
{
    public class Message
    {
        public string Status { get; set; }
        public IEnumerable<Signal> Signals { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}