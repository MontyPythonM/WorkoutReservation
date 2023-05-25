using Microsoft.Extensions.Options;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using WorkoutReservation.API.Settings;
using WorkoutReservation.API.Swagger;
using WorkoutReservation.Infrastructure;

namespace WorkoutReservation.API;

public static class Extensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        
        return services;
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var corsSettings = configuration.GetOptions<CorsSettings>(CorsSettings.SectionName);
        
        services.AddCors(options =>
        {
            options.AddPolicy(corsSettings.PolicyName, policy =>
            {
                policy.WithOrigins(corsSettings.OriginUrl)
                    .AllowAnyHeader()
                    .WithMethods(corsSettings.AllowedMethods)
                    .AllowCredentials();
            });
        });
        
        return services;
    }

    public static WebApplicationBuilder AddNLog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Host.UseNLog();
        return builder;
    }
}