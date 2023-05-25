using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using WorkoutReservation.API.Middleware;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Authentication;
using WorkoutReservation.Infrastructure.Authorization;
using WorkoutReservation.Infrastructure.BackgroundJobs;
using WorkoutReservation.Infrastructure.Identity;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Infrastructure.Persistence.Interceptors;
using WorkoutReservation.Infrastructure.Repositories;
using WorkoutReservation.Infrastructure.Services;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationSettings = configuration.GetOptions<AuthenticationSettings>(AuthenticationSettings.SectionName);
        var systemAdministratorSettings = configuration.GetOptions<FirstAdministratorSettings>(FirstAdministratorSettings.SectionName);
        var sendgridSettings = configuration.GetOptions<SendgridEmailSettings>(SendgridEmailSettings.SectionName);
        var sqlServerSettings = configuration.GetOptions<SqlServerSettings>(SqlServerSettings.SectionName);

        services.AddSingleton(authenticationSettings);
        services.AddSingleton(systemAdministratorSettings);
        services.AddSingleton(sendgridSettings);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        
        services.AddDbContext<AppDbContext>((serviceProvider, optionsBuilder) =>
        {
            optionsBuilder.UseSqlServer(sqlServerSettings.ConnectionString)
                .AddInterceptors(serviceProvider.GetService<DomainEventsToMessagesInterceptor>())
                .AddInterceptors(serviceProvider.GetService<AuditableEntitiesInterceptor>());
        });
        
        services.AddAuthorization();
        services.AddSingleton<ExceptionHandlingMiddleware>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IAuthorProvider, AuthorProvider>();
        services.AddSingleton<DomainEventsToMessagesInterceptor>();
        services.AddSingleton<AuditableEntitiesInterceptor>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddHostedService<DatabaseSeedInitializer>();
        services.AddHttpContextAccessor();
        
        services.AddQuartzBackgroundJobs();
        services.AddQuartzHostedService();
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
        services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<IWorkoutTypeRepository, WorkoutTypeRepository>();
        services.AddScoped<IRepetitiveWorkoutRepository, RepetitiveWorkoutRepository>();
        services.AddScoped<IRealWorkoutRepository, RealWorkoutRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IWorkoutTypeTagRepository, WorkoutTypeTagRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IEmailBuilder, EmailBuilder>();
        
        return services;
    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName) 
        where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}