using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;
using WorkoutReservation.Infrastructure.Exceptions;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Infrastructure.Seeders.Data;
using WorkoutReservation.Infrastructure.Settings;
using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Infrastructure.Services;

internal sealed class DatabaseSeedInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly FirstAdministratorSettings _firstAdministratorSettings;

    public DatabaseSeedInitializer(IServiceProvider serviceProvider, FirstAdministratorSettings firstAdministratorSettings)
    {
        _serviceProvider = serviceProvider;
        _firstAdministratorSettings = firstAdministratorSettings;
    }
    
    public async Task StartAsync(CancellationToken token)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordManager = scope.ServiceProvider.GetRequiredService<IPasswordManager>();
        
        if (await context.Database.CanConnectAsync(token) is false)
        {
            throw new CannotConnectToDatabaseException();
        }
        
        var isAdministratorExist = await context.ApplicationUsers
            .AnyAsync(x => x.ApplicationRoles.Contains(ApplicationRole.SystemAdministrator), token);
        
        if (!isAdministratorExist)
        {        
            await SeedFirstAdministratorAsync(context, passwordManager, token);
        }

        if (await IsDatabaseEmptyAsync(context, token))
        {
            await SeedDataAsync(context, token);
            await SeedWorkoutTypeInstructorRelationAsync(context, token);
        }
    }
    
    public Task StopAsync(CancellationToken token) => Task.CompletedTask;

    private async Task SeedFirstAdministratorAsync(AppDbContext context, IPasswordManager passwordManager, 
        CancellationToken token)
    {
        var administrator = new ApplicationUser(
            _firstAdministratorSettings.Email, 
            _firstAdministratorSettings.FirstName, 
            _firstAdministratorSettings.LastName, 
            Gender.Unspecified, 
            null,
            passwordManager.Secure(_firstAdministratorSettings.Password)
            );
        
        var systemAdministratorRole = await context.ApplicationRoles
            .FirstOrDefaultAsync(x => x.Id == (int)Role.SystemAdministrator, token);

        if (systemAdministratorRole is null)
        {
            throw new InfrastructureException("Application role not exist");
        }
        
        administrator.SetRole(systemAdministratorRole);
        await context.AddAsync(administrator, token);
        await context.SaveChangesAsync(token);
    }
    
    private async Task SeedDataAsync(AppDbContext context, CancellationToken token)
    {
        var instructors = InstructorsData.Create();
        var workoutTypes = WorkoutTypesData.Create();
        var realWorkouts = RealWorkoutsData.Create(instructors, workoutTypes);
        var repetitiveWorkouts = RepetitiveWorkoutsData.Create(instructors, workoutTypes);

        await context.AddRangeAsync(instructors, token);
        await context.AddRangeAsync(workoutTypes, token);
        await context.AddRangeAsync(realWorkouts, token);
        await context.AddRangeAsync(repetitiveWorkouts, token);
        await context.SaveChangesAsync(token);
    }

    private async Task SeedWorkoutTypeInstructorRelationAsync(AppDbContext context, CancellationToken token)
    {
        var instructors = await context.Instructors.ToListAsync(token);
        var workoutTypes = await context.WorkoutTypes.ToListAsync(token);
        
        var relation = new List<WorkoutTypeInstructor>
        {
            new(instructors[0].Id, workoutTypes[0].Id),
            new(instructors[0].Id, workoutTypes[1].Id),
            new(instructors[1].Id, workoutTypes[3].Id),
            new(instructors[2].Id, workoutTypes[2].Id),
            new(instructors[2].Id, workoutTypes[3].Id), 
        };
        
        await context.AddRangeAsync(relation, token);
        await context.SaveChangesAsync(token);
    }
    
    private async Task<bool> IsDatabaseEmptyAsync(AppDbContext context, CancellationToken token)
    {
        var isEmpty = !await context.Instructors.AnyAsync(token) && !await context.WorkoutTypes.AnyAsync(token) &&
                      !await context.RealWorkouts.AnyAsync(token) && !await context.RepetitiveWorkouts.AnyAsync(token);

        return isEmpty;
    }
}