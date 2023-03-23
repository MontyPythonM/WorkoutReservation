using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration;

public class RepetitiveWorkoutConfiguration : IEntityTypeConfiguration<RepetitiveWorkout>
{
    public void Configure(EntityTypeBuilder<RepetitiveWorkout> builder)
    {
        builder.ToTable("RepetitiveWorkouts", "WorkoutReservation");

        builder.Property(x => x.DayOfWeek)
            .IsRequired()
            .HasConversion<string>();          
        
        builder.Property(x => x.StartTime)
            .HasTimeOnlyConversion()
            .IsRequired();
        
        builder.Property(x => x.EndTime)
            .HasTimeOnlyConversion()
            .IsRequired();
        
        builder.HasOne(x => x.WorkoutType).WithMany(x => x.RepetitiveWorkouts);
        builder.HasOne(x => x.Instructor).WithMany(x => x.RepetitiveWorkouts);
    }
}
