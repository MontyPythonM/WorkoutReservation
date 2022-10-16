using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Domain.Common;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Presistence;

// dotnet ef database update -s ../WorkoutReservation.API
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
    public DbSet<RepetitiveWorkout> RepetitiveWorkouts { get; set; }
    public DbSet<RealWorkout> RealWorkouts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
