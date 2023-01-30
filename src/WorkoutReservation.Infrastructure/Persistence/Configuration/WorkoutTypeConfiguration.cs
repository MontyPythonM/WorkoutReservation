using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence.Configuration;

public class WorkoutTypeConfiguration : IEntityTypeConfiguration<WorkoutType>
{
    public void Configure(EntityTypeBuilder<WorkoutType> builder)
    {       
        builder.ToTable("WorkoutTypes", "WorkoutReservation");

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(600)
            .IsRequired();

        builder.Property(x => x.Intensity)
            .IsRequired()
            .HasConversion<string>();
        
        builder.HasMany(x => x.Instructors)
            .WithMany(x => x.WorkoutTypes)
            .UsingEntity<WorkoutTypeInstructor>(

                i => i.HasOne(wi => wi.Instructor)
                .WithMany()
                .HasForeignKey(i => i.InstructorId)
                .OnDelete(DeleteBehavior.Cascade),

                w => w.HasOne(wi => wi.WorkoutType)
                .WithMany()
                .HasForeignKey(w => w.WorkoutTypeId)
                .OnDelete(DeleteBehavior.Cascade),

                wi => wi.HasKey(x => new { x.InstructorId, x.WorkoutTypeId })
            );
    }
}
