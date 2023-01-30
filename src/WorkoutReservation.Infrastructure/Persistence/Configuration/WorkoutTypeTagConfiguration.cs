using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration;

public class WorkoutTypeTagConfiguration : IEntityTypeConfiguration<WorkoutTypeTag>
{
    public void Configure(EntityTypeBuilder<WorkoutTypeTag> builder)
    {
        builder.ToTable("WorkoutTypeTags", "WorkoutReservation");

        builder.Property(x => x.Tag).IsRequired();
    }
}
