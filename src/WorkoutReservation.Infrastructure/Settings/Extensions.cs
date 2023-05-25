using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkoutReservation.Infrastructure.Settings;

internal static class Extensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationSettings = configuration.GetOptions<AuthenticationSettings>(AuthenticationSettings.SectionName);
        var systemAdministratorSettings = configuration.GetOptions<FirstAdministratorSettings>(FirstAdministratorSettings.SectionName);
        var sendgridSettings = configuration.GetOptions<SendgridEmailSettings>(SendgridEmailSettings.SectionName);

        services.AddSingleton(authenticationSettings);
        services.AddSingleton(systemAdministratorSettings);
        services.AddSingleton(sendgridSettings);
        
        return services;
    }
}