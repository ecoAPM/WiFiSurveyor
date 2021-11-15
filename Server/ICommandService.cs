using System.Diagnostics;
using System.Threading.Tasks;

namespace WiFiSurveyor;

public interface ICommandService
{
	Task<string> Run(ProcessStartInfo info);
}
