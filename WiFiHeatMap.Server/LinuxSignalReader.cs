using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WiFiHeatMap.Server
{
    public class LinuxSignalReader : ISignalReader
    {
        private readonly ICommandService _commandService;

        public LinuxSignalReader(ICommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<string> Read()
        {
            var info = new ProcessStartInfo("iw", "dev wlan0 scan");
            {
            };

            try
            {
                return await _commandService.Run(info);
            }
            catch (ExternalException e)
            {
                switch (e.ErrorCode)
                {
                    case 2:
                        throw new Exception($"Executable \"{info.FileName}\" was not found. Please ensure \"network-tools\" is installed.");
                    case 13:
                        throw new Exception($"Executable \"{info.FileName}\" was unable to run. Please ensure \"{Assembly.GetExecutingAssembly().FullName}\" is run as root.");
                    default:
                        throw e;
                }
            }
        }
    }
}