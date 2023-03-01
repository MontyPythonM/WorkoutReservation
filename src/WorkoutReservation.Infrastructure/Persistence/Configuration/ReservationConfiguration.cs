using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations", "WorkoutReservation");

        builder.Property(x => x.ReservationStatus).IsRequired();
        builder.Property(x => x.Note).HasMaxLength(3000);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.RealWorkout)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.RealWorkoutId);
    }
}
