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
		_logger.LogIf(LogLevel.Debug, "{now}: Starting \"{cmd} {args}\"...", DateTime.Now, info.FileName, info.Arguments);

		var process = _startProcess(info);
		if (process == null)
		{
			_logger.LogIf(LogLevel.Warning, "{now}: Could not start {cmd}", DateTime.Now, info.FileName);
			return await Task.FromResult(string.Empty);
		}

		_logger.LogIf(LogLevel.Debug, "{now}: \"{cmd} {args}\" started", DateTime.Now, info.FileName, info.Arguments);
		var complete = process.WaitForExit(Convert.ToUInt16(_timeout.TotalMilliseconds));

		if (complete)
		{
			_logger.LogIf(LogLevel.Debug, "{now}: Process ended successfully", DateTime.Now);
		}
		else
		{
			_logger.LogIf(LogLevel.Warning, "{now}: Process not complete after {time}, forcing to end...", DateTime.Now, _timeout);
			process.Kill(true);
		}

		return await process.StandardOutput.ReadToEndAsync();
	}
}