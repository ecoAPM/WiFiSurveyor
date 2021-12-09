using System.Diagnostics;

namespace WiFiSurveyor.Core;

public abstract class BrowserLauncher : IBrowserLauncher
{
	private readonly string _baseArgs;
	private readonly string _command;
	private readonly Func<ProcessStartInfo, Process?> _start;

	protected BrowserLauncher(Func<ProcessStartInfo, Process?> start, string command, string baseArgs = "")
	{
		_start = start;
		_command = command;
		_baseArgs = baseArgs;
	}

	public void Run(string url)
		=> _start(new ProcessStartInfo(_command, $"{_baseArgs} {url}".Trim()));
}