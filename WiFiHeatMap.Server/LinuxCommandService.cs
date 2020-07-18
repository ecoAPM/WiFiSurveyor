using System.Diagnostics;
using System.Threading.Tasks;

namespace WiFiHeatMap.Server
{
    public class LinuxCommandService : ICommandService
    {
        public async Task<string> Run(ProcessStartInfo info)
        {
            info.RedirectStandardOutput = true;
            var process = Process.Start(info);
            process?.WaitForExit();

            return await (process?.StandardOutput.ReadToEndAsync() ?? Task.FromResult(string.Empty));
        }
    }
}