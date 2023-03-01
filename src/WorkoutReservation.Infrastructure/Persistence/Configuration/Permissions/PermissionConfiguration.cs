using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration.Permissions;

public class PermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
{
    public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
    {
        builder.ToTable("Permissions", "WorkoutReservation.Permissions");
        
        builder.HasKey(p => p.Id);
        builder.HasIndex(index => index.Name).IsUnique();
        
        builder.HasMany(r => r.ApplicationRoles)
            .WithMany(r => r.ApplicationPermissions)
            .UsingEntity<ApplicationRolePermission>();
        
        var permissions = Enum.GetValues<Permission>()
            .Select(p => new ApplicationPermission((int)p, p.ToString()));
        
        builder.HasData(permissions);
    }
}