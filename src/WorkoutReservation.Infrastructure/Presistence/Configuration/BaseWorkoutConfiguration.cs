using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Common;

namespace WorkoutReservation.Infrastructure.Presistence.Configuration;

public class BaseWorkoutConfiguration : IEntityTypeConfiguration<BaseWorkout>
{
    public void Configure(EntityTypeBuilder<BaseWorkout> builder)
    {
        builder.ToTable("Workouts");

        builder.Property(x => x.StartTime)
            .HasTimeOnlyConversion()
            .IsRequired();

        builder.Property(x => x.EndTime)
            .HasTimeOnlyConversion()
            .IsRequired();

        builder.HasOne(x => x.WorkoutType)
            .WithMany(x => x.BaseWorkouts)
            .HasForeignKey(x => x.WorkoutTypeId);

        builder.HasOne(x => x.Instructor)
            .WithMany(x => x.BaseWorkouts)
            .HasForeignKey(x => x.InstructorId);
    }
}
