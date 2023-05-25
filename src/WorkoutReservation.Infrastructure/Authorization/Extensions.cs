using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Infrastructure.Authorization;

internal static class Extensions
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddScoped<IPermissionService, PermissionService>();
        
        return services;
    }
}