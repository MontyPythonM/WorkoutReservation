using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration;

public class RepetitiveWorkoutConfiguration : IEntityTypeConfiguration<RepetitiveWorkout>
{
    public void Configure(EntityTypeBuilder<RepetitiveWorkout> builder)
    {
        builder.Property(x => x.DayOfWeek)
            .IsRequired()
            .HasConversion<string>();          
    }
}
