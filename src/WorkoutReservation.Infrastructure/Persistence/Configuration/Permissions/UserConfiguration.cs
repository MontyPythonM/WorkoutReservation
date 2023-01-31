using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration.Permissions;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users", "WorkoutReservation.Permissions");

        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();

        builder.Property(x => x.Gender)
            .HasDefaultValue(null)
            .HasConversion<string>();

        builder.Property(x => x.DateOfBirth)
            .HasDateOnlyConversion();
    }
}
