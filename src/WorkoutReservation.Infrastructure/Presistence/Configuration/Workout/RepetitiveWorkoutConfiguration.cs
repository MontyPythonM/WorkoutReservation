using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Infrastructure.Presistence.Configuration
{
    public class RepetitiveWorkoutConfiguration : IEntityTypeConfiguration<RepetitiveWorkout>
    {
        public void Configure(EntityTypeBuilder<RepetitiveWorkout> builder)
        {
            builder.Property(x => x.DayOfWeek).IsRequired();
        }
    }
}
