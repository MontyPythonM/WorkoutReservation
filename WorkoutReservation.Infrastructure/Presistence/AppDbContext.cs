using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence.Configuration;

namespace WorkoutReservation.Infrastructure.Presistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<WorkoutType> WorkoutTypes { get; set; }
        public DbSet<WorkoutTypeInstructor> WorkoutTypeInstructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new InstructorConfiguration().Configure(modelBuilder.Entity<Instructor>()); 
            new WorkoutTypeConfiguration().Configure(modelBuilder.Entity<WorkoutType>());
        }
    }
}
