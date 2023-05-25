using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence.Interceptors;
using WorkoutReservation.Infrastructure.Repositories;
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
        
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<IWorkoutTypeRepository, WorkoutTypeRepository>();
        services.AddScoped<IRepetitiveWorkoutRepository, RepetitiveWorkoutRepository>();
        services.AddScoped<IRealWorkoutRepository, RealWorkoutRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IWorkoutTypeTagRepository, WorkoutTypeTagRepository>();
        
        return services;
    }
}