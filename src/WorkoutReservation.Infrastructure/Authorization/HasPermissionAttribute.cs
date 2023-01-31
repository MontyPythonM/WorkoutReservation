using Microsoft.AspNetCore.Authorization;

namespace WorkoutReservation.Infrastructure.Authorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission) 
        : base(policy: permission.ToString())
    {
    }
}