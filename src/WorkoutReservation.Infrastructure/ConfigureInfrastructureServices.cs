using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Authentication;
using WorkoutReservation.Infrastructure.Authorization;
using WorkoutReservation.Infrastructure.Identity;
using WorkoutReservation.Infrastructure.Interfaces;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Infrastructure.Repositories;
using WorkoutReservation.Infrastructure.Seeders;
using WorkoutReservation.Infrastructure.Seeders.Data;
using WorkoutReservation.Infrastructure.Services;
using WorkoutReservation.Infrastructure.Settings;

namespace WorkoutReservation.Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //--- JWT authentication settings configuration
        var authenticationSettings = new AuthenticationSettings();
        configuration.GetSection("Authentication").Bind(authenticationSettings);

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
        
        var systemAdministratorSettings = new SystemAdministratorSettings();
        configuration.GetSection("FirstAdmin").Bind(systemAdministratorSettings);
        
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddSingleton(authenticationSettings);
        services.AddSingleton(systemAdministratorSettings);

        services.AddHttpContextAccessor();
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("localDbConnection")));

        services.AddHangfire(options =>
            options.UseSqlServerStorage(configuration.GetConnectionString("localDbConnection")));

        services.AddHangfireServer();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
        services.AddScoped<IAuthorProvider, AuthorProvider>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IApplicationRoleRepository, ApplicationRoleRepository>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<IWorkoutTypeRepository, WorkoutTypeRepository>();
        services.AddScoped<IRepetitiveWorkoutRepository, RepetitiveWorkoutRepository>();
        services.AddScoped<IRealWorkoutRepository, RealWorkoutRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IWorkoutTypeTagRepository, WorkoutTypeTagRepository>();
        services.AddScoped<IFirstSystemAdministrator, FirstSystemAdministrator>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<SystemAdministratorSeeder>();
        services.AddScoped<ApplicationDataSeeder>();
        
        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 , LogEvents = true });

        return services;
    }
}
