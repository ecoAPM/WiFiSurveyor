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

		var output = process.StandardOutput.ReadToEndAsync();
		using var cancellation = new CancellationTokenSource(_timeout);
		try
		{
			await process.WaitForExitAsync(cancellation.Token);
			logger.LogIf(LogLevel.Debug, "{now}: Process ended successfully", DateTime.Now);
		}
		catch (OperationCanceledException)
		{
			if (!process.HasExited)
				process.Kill(true);
			await process.WaitForExitAsync();
			logger.LogIf(LogLevel.Warning, "{now}: Process not completed after {time}, forced to end...", DateTime.Now, _timeout);
		}

		return await output;
	}
}
