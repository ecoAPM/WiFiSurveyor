using System.Diagnostics;
using System.Threading.Tasks;

namespace WiFiSurveyor.Core;

public interface ICommandService
{
	Task<string> Run(ProcessStartInfo info);
}