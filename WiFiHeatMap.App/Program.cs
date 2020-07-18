using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace WiFiHeatMap.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var blazor = WebAssemblyHostBuilder.CreateDefault(args);
            blazor.RootComponents.Add<Main>("app");
            await blazor.Build().RunAsync();
        }
    }
}
