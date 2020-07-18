using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WiFiHeatMap.Server
{
    public class LinuxSignalParser : ISignalParser
    {
        public IEnumerable<Signal> Parse(string results)
        {
            var accessPoints = results
                .Split($"{Environment.NewLine}BSS").AsEnumerable()
                .Where(r => !string.IsNullOrWhiteSpace(r));

            var signals = new List<Signal>();
            foreach (var result in accessPoints)
            {
                var ssid = Regex.Match(result, $"SSID: (.+){Environment.NewLine}").Groups[1].Value;
                var freq = Regex.Match(result, $"freq: (.+){Environment.NewLine}").Groups[1].Value;
                var dbm = Regex.Match(result, "signal: (-.*) dBm").Groups[1].Value;

                var signal = new Signal
                {
                    SSID = ssid,
                    Frequency = (freq[0] == '2' ? Frequency._2_4_GHz : Frequency._5_GHz),
                    Strength = decimal.Parse(dbm)
                };

                signals.Add(signal);
            }
            return signals;
        }
    }
}