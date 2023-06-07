using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutReservation.API.Middleware;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Infrastructure.Authentication;
using WorkoutReservation.Infrastructure.Authorization;
using WorkoutReservation.Infrastructure.BackgroundJobs;
using WorkoutReservation.Infrastructure.Identity;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Infrastructure.Repositories;
using WorkoutReservation.Infrastructure.Security;
using WorkoutReservation.Infrastructure.Services;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure;

public static class Extensions
{
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSecurity()
            .AddAuthenticationServices(configuration)
            .AddAuthorizationServices()
            .AddSqlServerDatabase(configuration)
            .AddRepositories()
            .AddSettings(configuration)
            .AddQuartzBackgroundJobs();

        services.AddSingleton<ExceptionHandlingMiddleware>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddSingleton<IAuthorProvider, AuthorProvider>();
        
        services.AddHttpContextAccessor();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IEmailBuilder, EmailBuilder>();
        
        return services;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) 
        where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}