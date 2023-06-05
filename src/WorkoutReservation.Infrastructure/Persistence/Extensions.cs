using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutReservation.Infrastructure.Persistence.Interceptors;
using WorkoutReservation.Infrastructure.Services;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure.Persistence;

internal static class Extensions
{
    public static IServiceCollection AddSqlServerDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SqlServerSettings>(configuration.GetRequiredSection(SqlServerSettings.SectionName));
        var sqlServerSettings = configuration.GetOptions<SqlServerSettings>(SqlServerSettings.SectionName);

        services.AddDbContext<AppDbContext>((serviceProvider, optionsBuilder) =>
        {
            optionsBuilder.UseSqlServer(sqlServerSettings.ConnectionString)
                .AddInterceptors(serviceProvider.GetService<DomainEventsToMessagesInterceptor>())
                .AddInterceptors(serviceProvider.GetService<AuditableEntitiesInterceptor>());
        });
        
        services.AddSingleton<DomainEventsToMessagesInterceptor>();
        services.AddSingleton<AuditableEntitiesInterceptor>();
        services.AddHostedService<DatabaseSeedInitializer>();

        return services;
    }
}