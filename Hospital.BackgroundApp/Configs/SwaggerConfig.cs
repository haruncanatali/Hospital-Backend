using Microsoft.OpenApi.Models;

namespace Hospital.BackgroundApp.Configs;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hospital.Queue.Api", Version = "v1" });
        });
        return services;
    }
}