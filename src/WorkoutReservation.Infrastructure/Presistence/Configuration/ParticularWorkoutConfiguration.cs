using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Infrastructure.Presistence.Configuration
{
    public class ParticularWorkoutConfiguration : IEntityTypeConfiguration<ParticularWorkout>
    {
        public void Configure(EntityTypeBuilder<ParticularWorkout> builder)
        {
            builder.Property(x => x.CurrentParticipianNumber).IsRequired();

            builder.Property(x => x.Date)
                .HasDateOnlyConversion()
                .IsRequired();

            //builder.Property(x => x.StartTime)
            //    .HasTimeOnlyConversion()
            //    .IsRequired();

            //builder.Property(x => x.EndTime)
            //    .HasTimeOnlyConversion()
            //    .IsRequired();
        }
    }
}
