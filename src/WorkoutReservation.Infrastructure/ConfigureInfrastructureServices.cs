using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

namespace WorkoutReservation.Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
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

        return services;
    }
}
