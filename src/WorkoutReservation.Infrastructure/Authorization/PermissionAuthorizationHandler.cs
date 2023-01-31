using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using WorkoutReservation.Application.Contracts;

namespace WorkoutReservation.Infrastructure.Authorization;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        var currentUserAccessor = scope.ServiceProvider.GetRequiredService<ICurrentUserAccessor>();
        
        var permissions = await permissionService
            .GetPermissionsAsync(currentUserAccessor.GetCurrentUserId());

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}