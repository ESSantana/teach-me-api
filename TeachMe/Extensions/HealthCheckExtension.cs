using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.API.Extensions
{
    public static class HealthCheckExtension
    {
        public static void ConfigureHealthCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthy", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("app_tag"),
                ResponseWriter = WriteResponse
            });
        }

        private async static Task WriteResponse(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var result = JsonConvert.SerializeObject(
                new
                {
                    statusApplication = report.Status.ToString(),
                    healthChecks = report.Entries.Select(e => new
                    {
                        check = e.Key,
                        ErrorMessage = e.Value.Exception?.Message,
                        statusMessage = e.Value.Description,
                        status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                    })
                });

            await context.Response.WriteAsync(result);
        }
    }
}
