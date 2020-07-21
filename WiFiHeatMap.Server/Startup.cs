using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WiFiHeatMap.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWiFiHeatMap();
            services.AddCors();
            services.AddLogging();
            services.AddResponseCompression();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseResponseCompression();
            app.UseRouting();
            app.UseEndpoints(builder => builder.MapHub<SignalHub>(""));
        }
    }
}
