using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors", "WorkoutReservation");

        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();          
        builder.Property(x => x.Email).IsRequired();

        builder.Property(x => x.Biography).HasMaxLength(3000);

        builder.Property(x => x.Gender).HasConversion<string>();
    }
}
