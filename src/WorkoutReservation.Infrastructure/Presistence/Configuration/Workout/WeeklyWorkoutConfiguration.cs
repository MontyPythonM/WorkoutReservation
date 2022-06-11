using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Infrastructure.Presistence.Configuration
{
    public class WeeklyWorkoutConfiguration : IEntityTypeConfiguration<WeeklyWorkout>
    {
        public void Configure(EntityTypeBuilder<WeeklyWorkout> builder)
        {
            builder.Property(x => x.DayOfWeek).IsRequired();
        }
    }
}
