using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Domain.Entities;

public sealed class ApplicationRolePermission
{
    public int ApplicationRoleId { get; }
    public int ApplicationPermissionId { get; }

    public ApplicationRolePermission(ApplicationRole role, Permission permission)
    {
        ApplicationRoleId = role.Id;
        ApplicationPermissionId = (int)permission;
    }

    private ApplicationRolePermission()
    {
        // required by EF core
    }
}