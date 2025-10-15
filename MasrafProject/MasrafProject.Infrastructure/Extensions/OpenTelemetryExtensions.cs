using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MasrafProject.Infrastructure.Extensions;  

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddCustomOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenTelemetry()
            .WithTracing(builder =>
            {
                builder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation() 
                    .AddJaegerExporter()
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MasrafProject.API"));
            });
        return services;
    }
}

