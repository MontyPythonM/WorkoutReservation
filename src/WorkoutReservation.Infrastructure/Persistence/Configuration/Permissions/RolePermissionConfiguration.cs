using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration.Permissions;

public class RolePermissionConfiguration : IEntityTypeConfiguration<ApplicationRolePermission>
{
    public void Configure(EntityTypeBuilder<ApplicationRolePermission> builder)
    {
        builder.ToTable("RolesPermissions", "WorkoutReservation.Permissions");

        builder.HasKey(x => new { x.ApplicationRoleId, x.ApplicationPermissionId });
        
        builder.HasData(RolePermissionMatrix.Create());
    }
}