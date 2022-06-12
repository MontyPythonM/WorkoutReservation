using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Presistence.Configuration
{
    public class WorkoutTypeConfiguration : IEntityTypeConfiguration<WorkoutType>
    {
        public void Configure(EntityTypeBuilder<WorkoutType> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(600).IsRequired();
            builder.Property(x => x.Intensity).IsRequired();

            builder.HasMany(x => x.Instructors)
                .WithMany(x => x.WorkoutTypes)
                .UsingEntity<WorkoutTypeInstructor>(

                    i => i.HasOne(wi => wi.Instructor)
                    .WithMany()
                    .HasForeignKey(i => i.InstructorId)
                    .OnDelete(DeleteBehavior.NoAction),

                    w => w.HasOne(wi => wi.WorkoutType)
                    .WithMany()
                    .HasForeignKey(w => w.WorkoutTypeId)
                    .OnDelete(DeleteBehavior.NoAction),

                    wi => wi.HasKey(x => new { x.InstructorId, x.WorkoutTypeId })
                );

            builder.HasMany(x => x.WorkoutTypeTags)
                .WithOne(x => x.WorkoutType)
                .HasForeignKey(x => x.WorkoutTypeId);
        }
    }
}
