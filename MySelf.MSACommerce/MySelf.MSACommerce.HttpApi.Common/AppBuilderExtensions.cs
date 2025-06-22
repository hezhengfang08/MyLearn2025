using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySelf.MSACommerce.Consul.ServiceRegistration;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.HttpApi.Common
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseHttpCommon(this IApplicationBuilder app) 
        {
            app.UseMetricServer();
            app.UseHttpMetrics();

            app.UseCors("AllowAny");

            var serviceCheck = app.ApplicationServices.GetRequiredService<IOptions<ServiceCheckConfiguration>>().Value;
            app.UseHealthChecks(serviceCheck.Path);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseExceptionHandler(_ => { });

            return app;
        }
    }
}
