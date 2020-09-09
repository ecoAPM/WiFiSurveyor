using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WiFiSurveyor
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

            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug($"{DateTime.Now}: Starting \"{info.FileName} {info.Arguments}\"...");
            var process = _startProcess(info);
            if (process == null)
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                    _logger.LogWarning($"{DateTime.Now}: Could not start {info.FileName}");
                return await Task.FromResult(string.Empty);
            }
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug($"{DateTime.Now}: \"{info.FileName} {info.Arguments}\" started");

            var complete = process.WaitForExit(Convert.ToUInt16(_timeout.TotalMilliseconds));
            if (complete)
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                    _logger.LogDebug($"{DateTime.Now}: Process ended successfully");
            }
            else
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                    _logger.LogWarning($"{DateTime.Now}: Process not complete after {_timeout}, forcing to end...");
                process.Kill(true);
            }

            return await process.StandardOutput.ReadToEndAsync();
        }
    }
}