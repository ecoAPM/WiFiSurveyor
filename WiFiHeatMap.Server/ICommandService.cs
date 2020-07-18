using System.Diagnostics;
using System.Threading.Tasks;

namespace WiFiHeatMap.Server
{
    public interface ICommandService
    {
        Task<string> Run(ProcessStartInfo info);
    }
}