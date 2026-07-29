using System.Diagnostics;

namespace WiFiSurveyor.Core;

public sealed class CommandService(Func<ProcessStartInfo, Process?> startProcess, ILogger logger, TimeSpan? timeout = null) : ICommandService
{
	private readonly TimeSpan _timeout = timeout ?? TimeSpan.FromSeconds(10);

	public async Task<string> Run(ProcessStartInfo info)
	{
		info.RedirectStandardOutput = true;
		logger.LogIf(LogLevel.Debug, "{now}: Starting \"{cmd} {args}\"...", DateTime.Now, info.FileName, info.Arguments);

		var process = startProcess(info);
		if (process == null)
		{
			logger.LogIf(LogLevel.Warning, "{now}: Could not start {cmd}", DateTime.Now, info.FileName);
			return await Task.FromResult(string.Empty);
		}

		logger.LogIf(LogLevel.Debug, "{now}: \"{cmd} {args}\" started", DateTime.Now, info.FileName, info.Arguments);
		var complete = process.WaitForExit(Convert.ToUInt16(_timeout.TotalMilliseconds));

		if (complete)
		{
			logger.LogIf(LogLevel.Debug, "{now}: Process ended successfully", DateTime.Now);
		}
		else
		{
			logger.LogIf(LogLevel.Warning, "{now}: Process not complete after {time}, forcing to end...", DateTime.Now, _timeout);
			process.Kill(true);
		}

		return await process.StandardOutput.ReadToEndAsync();
	}
}