using System.Diagnostics;

namespace WiFiSurveyor.Core;

public sealed class CommandService(Func<ProcessStartInfo, Process?> startProcess, ILogger logger, TimeSpan? timeout = null) : ICommandService
{
	private readonly TimeSpan _timeout = timeout ?? TimeSpan.FromSeconds(10);

	public async Task<string> Run(ProcessStartInfo info)
	{
		logger.LogIf(LogLevel.Debug, "{now}: Starting \"{cmd} {args}\"...", DateTime.Now, info.FileName, info.Arguments);
		info.RedirectStandardOutput = true;
		using var process = startProcess(info);

		if (process == null)
		{
			logger.LogIf(LogLevel.Warning, "{now}: Could not start {cmd}", DateTime.Now, info.FileName);
			return await Task.FromResult(string.Empty);
		}

		logger.LogIf(LogLevel.Debug, "{now}: \"{cmd} {args}\" started", DateTime.Now, info.FileName, info.Arguments);

		var msTimeout = Convert.ToUInt16(_timeout.TotalMilliseconds);
		var complete = process.WaitForExit(msTimeout);

		if (complete)
		{
			var output = await process.StandardOutput.ReadToEndAsync();
			logger.LogIf(LogLevel.Debug, "{now}: Process ended successfully", DateTime.Now);
			return output;
		}

		process.Kill(true);
		var truncatedOutput = await process.StandardOutput.ReadToEndAsync();

		if (!string.IsNullOrEmpty(truncatedOutput))
			logger.LogIf(LogLevel.Debug, "{now}: Process stuck but {size} bytes of output received", DateTime.Now, truncatedOutput.Length);
		else
			logger.LogIf(LogLevel.Warning, "{now}: Process not completed after {time}, forced to end...", DateTime.Now, _timeout);

		return truncatedOutput;
	}
}