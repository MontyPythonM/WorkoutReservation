using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyHelpers.EntityFrameworkCore.Extensions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration;

public class RealWorkoutConfiguration : IEntityTypeConfiguration<RealWorkout>
{
    public void Configure(EntityTypeBuilder<RealWorkout> builder)
    {
        builder.Property(x => x.CurrentParticipantNumber)
            .IsRequired();

        builder.Property(x => x.Date)
            .HasDateOnlyConversion()
            .IsRequired();
    }
}
