using System.Diagnostics;

namespace WiFiSurveyor.Core;

public interface ICommandService
{
	Task<string> Run(ProcessStartInfo info);
}