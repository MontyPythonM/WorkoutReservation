using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Interfaces;

namespace WorkoutReservation.Infrastructure.Persistence;

// dotnet ef database update -s ../WorkoutReservation.API
// dotnet ef migrations add <name> -s../WorkoutReservation.API
public sealed class AppDbContext : DbContext
{
    private readonly IAuthorProvider _authorProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IAuthorProvider authorProvider, 
        IDateTimeProvider dateTimeProvider) : base(options)
    {
        _authorProvider = authorProvider;
        _dateTimeProvider = dateTimeProvider;
    }

    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<WorkoutType> WorkoutTypes { get; set; }
    public DbSet<WorkoutTypeInstructor> WorkoutTypeInstructors { get; set; }
    public DbSet<WorkoutTypeTag> WorkoutTypeTags { get; set; }
    public DbSet<BaseWorkout> BaseWorkouts { get; set; }
    public DbSet<RepetitiveWorkout> RepetitiveWorkouts { get; set; }
    public DbSet<RealWorkout> RealWorkouts { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        UpdateAuditableEntities(_dateTimeProvider.Now, _authorProvider.GetAuthor());
        return await base.SaveChangesAsync(token);
    }
    
    private void UpdateAuditableEntities(DateTime now, string author)
    {
        foreach (var entityEntry in ChangeTracker.Entries<Entity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(Entity.CreatedDate)).CurrentValue = now;
                entityEntry.Property(nameof(Entity.CreatedBy)).CurrentValue = author;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(Entity.LastModifiedDate)).CurrentValue = now;
                entityEntry.Property(nameof(Entity.LastModifiedBy)).CurrentValue = author;
            }
        }
    }
}
