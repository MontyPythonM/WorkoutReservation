using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Domain.Common;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Entities.Workout;
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
        public DbSet<WorkoutTypeTag> WorkoutTypeTags { get; set; }

        public DbSet<BaseWorkout> BaseWorkouts { get; set; }
        public DbSet<WeeklyWorkout> WeeklyWorkouts { get; set; }
        public DbSet<ParticularWorkout> ParticularWorkouts { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new InstructorConfiguration().Configure(modelBuilder.Entity<Instructor>()); 
            new WorkoutTypeConfiguration().Configure(modelBuilder.Entity<WorkoutType>());
            new WorkoutTypeTagConfiguration().Configure(modelBuilder.Entity<WorkoutTypeTag>());
            new BaseWorkoutConfiguration().Configure(modelBuilder.Entity<BaseWorkout>());
            new WeeklyWorkoutConfiguration().Configure(modelBuilder.Entity<WeeklyWorkout>());
            new ParticularWorkoutConfiguration().Configure(modelBuilder.Entity<ParticularWorkout>());

        }
    }
}
