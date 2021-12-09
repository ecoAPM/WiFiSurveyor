using System.Diagnostics;

namespace WiFiSurveyor.Core;

public abstract class BrowserLauncher
{
	private readonly string _command;
	private readonly string _baseArgs;

	protected BrowserLauncher(string command, string baseArgs = "")
	{
		_command = command;
		_baseArgs = baseArgs;
	}

	public void Run(string url)
		=> Process.Start(new ProcessStartInfo(_command, $"{_baseArgs} {url}".Trim()));
}