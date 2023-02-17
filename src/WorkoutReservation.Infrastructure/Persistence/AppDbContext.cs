﻿using Microsoft.EntityFrameworkCore;
using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Persistence;

// dotnet ef database update -s ../WorkoutReservation.API
// dotnet ef migrations add <name> -s../WorkoutReservation.API
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
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}