using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration;

public class RealWorkoutConfiguration : IEntityTypeConfiguration<RealWorkout>
{
    public void Configure(EntityTypeBuilder<RealWorkout> builder)
    {
        builder.ToTable("RealWorkouts", "WorkoutReservation");

        builder.Property(x => x.CurrentParticipantNumber)
            .IsRequired();

        builder.Property(x => x.Date)
            .HasDateOnlyConversion()
            .IsRequired();
        
        builder.Property(x => x.StartTime)
            .HasTimeOnlyConversion()
            .IsRequired();
        
        builder.Property(x => x.EndTime)
            .HasTimeOnlyConversion()
            .IsRequired();

        builder.HasOne(x => x.WorkoutType).WithMany(x => x.RealWorkouts);
        builder.HasOne(x => x.Instructor).WithMany(x => x.RealWorkouts);
    }
}
