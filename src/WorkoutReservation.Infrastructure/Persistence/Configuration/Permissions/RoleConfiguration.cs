using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;

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

        builder.HasData(ApplicationRole.GetEnumerations());
    }
}