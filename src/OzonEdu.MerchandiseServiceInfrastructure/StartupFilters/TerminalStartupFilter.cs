using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OzonEdu.MerchandiseServiceInfrastructure.Middlewares;

namespace OzonEdu.MerchandiseServiceInfrastructure.StartupFilters
{
    public class TerminalStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/live", b => b.Run(async c => await c.Response.CompleteAsync()));
                app.Map("/ready", b => b.Run(async c => await c.Response.CompleteAsync()));
                app.Map("/version", b =>b.Run(
                    async c => {
                        var response = new {
                            version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version",
                            serviceName = Assembly.GetExecutingAssembly().GetName().Name
                        };
                        await c.Response.WriteAsJsonAsync(response);
                    }));
                
                app.UseMiddleware<LoggingMiddleware>();
                next(app);
            };
        }
    }
}