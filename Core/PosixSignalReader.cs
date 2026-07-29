using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace WiFiSurveyor.Core;

public abstract class PosixSignalReader(ICommandService commandService) : ISignalReader<string>
{
	protected abstract ProcessStartInfo Info { get; }

	public async Task<string> Read()
	{
		try
		{
			return await commandService.Run(Info);
		}
		catch (Win32Exception e)
		{
			switch (e.NativeErrorCode)
			{
				case 2:
					var msg = $"Executable \"{Info.FileName}\" was not found. Please ensure \"wireless-tools\" is installed and \"{Assembly.GetExecutingAssembly().GetName().Name}\" is running as root.";
					throw new FileNotFoundException(msg, Info.FileName, e);
				default:
					throw;
			}
		}
	}
}