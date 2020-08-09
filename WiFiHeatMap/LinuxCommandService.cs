using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WiFiHeatMap
{
    public class LinuxCommandService : ICommandService
    {
        private readonly Func<ProcessStartInfo, Process> _startProcess;
        private readonly ILogger _logger;
        private readonly TimeSpan _timeout;

        public LinuxCommandService(Func<ProcessStartInfo, Process> startProcess, ILogger logger, TimeSpan? timeout = null)
        {
            _startProcess = startProcess;
            _logger = logger;
            _timeout = timeout ?? TimeSpan.FromSeconds(10);
        }

        public async Task<string> Run(ProcessStartInfo info)
        {
            info.RedirectStandardOutput = true;

            _logger.LogDebug($"{DateTime.Now}: Starting \"{info.FileName} {info.Arguments}\"...");
            var process = _startProcess(info);
            if (process == null)
            {
                _logger.LogWarning($"{DateTime.Now}: Could not start {info.FileName}");
                return await Task.FromResult(string.Empty);
            }
            _logger.LogDebug($"{DateTime.Now}: \"{info.FileName} {info.Arguments}\" started");

            var complete = process.WaitForExit(Convert.ToUInt16(_timeout.TotalMilliseconds));
            if (complete)
            {
                _logger.LogDebug($"{DateTime.Now}: Process ended successfully");
            }
            else
            {
                _logger.LogWarning($"{DateTime.Now}: Process not complete after {_timeout}, forcing to end...");
                process.Kill(true);
            }

            return await process.StandardOutput.ReadToEndAsync();
        }
    }
}