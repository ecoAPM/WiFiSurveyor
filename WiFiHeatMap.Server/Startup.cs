using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WiFiHeatMap.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    services.AddSingleton<ICommandService, LinuxCommandService>();
                    services.AddSingleton<ISignalReader, LinuxSignalReader>();
                    services.AddSingleton<ISignalParser, LinuxSignalParser>();
                    break;
                default:
                    throw new NotImplementedException($"{Environment.OSVersion.Platform} is not currently supported");
            }

            services.AddSingleton<ISignalHub, SignalHub>();
            services.AddHostedService<SignalService>();

            services.AddCors();
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
