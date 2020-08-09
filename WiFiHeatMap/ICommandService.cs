using System.Diagnostics;
using System.Threading.Tasks;

namespace WiFiHeatMap
{
    public interface ICommandService
    {
        Task<string> Run(ProcessStartInfo info);
    }
}