using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Infrastructure.Presistence.Configuration
{
    public class RealWorkoutConfiguration : IEntityTypeConfiguration<RealWorkout>
    {
        public void Configure(EntityTypeBuilder<RealWorkout> builder)
        {
            builder.Property(x => x.CurrentParticipianNumber)
                .IsRequired();

            builder.Property(x => x.Date)
                .HasDateOnlyConversion()
                .IsRequired();
        }
    }
}
