using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WiFiSurveyor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWiFiSurveyor();
            services.AddCors();
            services.AddLogging();
            services.AddResponseCompression();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder.AllowCredentials().AllowAnyHeader().WithOrigins("http://localhost:3000"));
            app.UseResponseCompression();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(builder => builder.MapHub<SignalHub>("/signals"));
        }
    }
}
