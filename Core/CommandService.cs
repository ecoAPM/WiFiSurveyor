using System.Diagnostics;

namespace WiFiSurveyor.Core;

public sealed class CommandService : ICommandService
{
	private readonly ILogger _logger;
	private readonly Func<ProcessStartInfo, Process?> _startProcess;
	private readonly TimeSpan _timeout;

	public CommandService(Func<ProcessStartInfo, Process?> startProcess, ILogger logger, TimeSpan? timeout = null)
	{
		_startProcess = startProcess;
		_logger = logger;
		_timeout = timeout ?? TimeSpan.FromSeconds(10);
	}

	public async Task<string> Run(ProcessStartInfo info)
	{
		info.RedirectStandardOutput = true;

		if (_logger.IsEnabled(LogLevel.Debug))
		{
			_logger.LogDebug("{now}: Starting \"{cmd} {args}\"...", DateTime.Now, info.FileName, info.Arguments);
		}

		var process = _startProcess(info);
		if (process == null)
		{
			if (_logger.IsEnabled(LogLevel.Warning))
			{
				_logger.LogWarning("{now}: Could not start {cmd}", DateTime.Now, info.FileName);
			}

			return await Task.FromResult(string.Empty);
		}

		if (_logger.IsEnabled(LogLevel.Debug))
		{
			_logger.LogDebug("{now}: \"{cmd} {args}\" started", DateTime.Now, info.FileName, info.Arguments);
		}

		var complete = process.WaitForExit(Convert.ToUInt16(_timeout.TotalMilliseconds));
		if (complete)
		{
			if (_logger.IsEnabled(LogLevel.Debug))
			{
				_logger.LogDebug("{now}: Process ended successfully", DateTime.Now);
			}
		}
		else
		{
			if (_logger.IsEnabled(LogLevel.Warning))
			{
				_logger.LogWarning("{now}: Process not complete after {time}, forcing to end...", DateTime.Now, _timeout);
			}

			process.Kill(true);
		}

		return await process.StandardOutput.ReadToEndAsync();
	}
}