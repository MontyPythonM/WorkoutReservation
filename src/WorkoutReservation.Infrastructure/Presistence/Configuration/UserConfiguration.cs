using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Presistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired(); 
        
        builder.Property(x => x.UserRole)
            .IsRequired()
            .HasDefaultValue(null)
            .HasConversion<string>();

        builder.Property(x => x.Gender)
            .HasDefaultValue(null)
            .HasConversion<string>();

        builder.Property(x => x.DateOfBirth)
            .HasDateOnlyConversion();
    }
}
