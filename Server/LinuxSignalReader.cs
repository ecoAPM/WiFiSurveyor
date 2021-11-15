using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace WiFiSurveyor;

public class LinuxSignalReader : ISignalReader<string>
{
	private readonly ICommandService _commandService;

	public LinuxSignalReader(ICommandService commandService)
	{
		_commandService = commandService;
	}

	public async Task<string> Read()
	{
		var info = new ProcessStartInfo("iwlist", "wlan0 scanning");

		try
		{
			return await _commandService.Run(info);
		}
		catch (Win32Exception e)
		{
			switch (e.NativeErrorCode)
			{
				case 2:
					var msg = $"Executable \"{info.FileName}\" was not found. Please ensure \"wireless-tools\" is installed and \"{Assembly.GetExecutingAssembly().GetName().Name}\" is running as root.";
					throw new FileNotFoundException(msg, info.FileName, e);
				default:
					throw;
			}
		}
	}
}
