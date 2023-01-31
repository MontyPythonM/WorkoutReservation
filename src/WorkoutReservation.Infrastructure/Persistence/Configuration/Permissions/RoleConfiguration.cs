using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Authorization;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration.Permissions;

internal class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Roles", "WorkoutReservation.Permissions");
        
        builder.HasKey(r => r.Id);
        builder.HasIndex(index => index.Name).IsUnique();
        
        builder.HasMany(r => r.ApplicationUsers)
            .WithMany(r => r.ApplicationRoles)
            .UsingEntity<ApplicationUserRole>();

        builder.HasData(ApplicationRole.Create());
    }
}