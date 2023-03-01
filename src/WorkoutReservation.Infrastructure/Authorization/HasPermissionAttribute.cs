using Microsoft.AspNetCore.Authorization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Authorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission) 
        : base(policy: permission.ToString())
    {
    }
}