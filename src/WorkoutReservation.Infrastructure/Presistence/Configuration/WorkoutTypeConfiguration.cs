using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Presistence.Configuration
{
    public class WorkoutTypeConfiguration : IEntityTypeConfiguration<WorkoutType>
    {
        public void Configure(EntityTypeBuilder<WorkoutType> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Intensity).IsRequired();

            builder.HasMany(x => x.Instructors)
                .WithMany(x => x.WorkoutTypes)
                .UsingEntity<WorkoutTypeInstructor>(

                    i => i.HasOne(wi => wi.Instructor)
                    .WithMany()
                    .HasForeignKey(i => i.InstructorId),

                    i => i.HasOne(wi => wi.WorkoutType)
                    .WithMany()
                    .HasForeignKey(i => i.WorkoutTypeId),

                    wi => wi.HasKey(x => 
                        new { x.InstructorId, x.WorkoutTypeId })
                );

            builder.HasMany(x => x.WorkoutTypeTags)
                .WithOne(x => x.WorkoutType)
                .HasForeignKey(x => x.WorkoutTypeId);
        }
    }
}
