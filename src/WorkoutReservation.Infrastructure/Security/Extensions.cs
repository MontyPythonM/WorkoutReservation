using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddSingleton<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>()
            .AddSingleton<IPasswordManager, PasswordManager>();

        return services;
    }
}