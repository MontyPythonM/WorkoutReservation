using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Persistence;
using WorkoutReservation.Infrastructure.Seeders.Data;

namespace WorkoutReservation.Infrastructure.Seeders;

public class ApplicationDataSeeder
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ApplicationDataSeeder> _logger;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IWorkoutTypeRepository _workoutTypeRepository;

    public ApplicationDataSeeder(AppDbContext dbContext, 
        ILogger<ApplicationDataSeeder> logger, 
        IInstructorRepository instructorRepository, 
        IWorkoutTypeRepository workoutTypeRepository)
    {
        _dbContext = dbContext;
        _logger = logger;
        _instructorRepository = instructorRepository;
        _workoutTypeRepository = workoutTypeRepository;
    }

    public async Task SeedAsync(CancellationToken token)
    {
        if (await _dbContext.Database.CanConnectAsync(token) is false)
            throw new DatabaseConnectionException();
        
        if (await _dbContext.Instructors.AnyAsync(token) || await _dbContext.WorkoutTypes.AnyAsync(token) ||
            await _dbContext.RealWorkouts.AnyAsync(token) || await _dbContext.RepetitiveWorkouts.AnyAsync(token))
        {        
            return;
        }

        try
        {
            await Task.WhenAll(
                _dbContext.AddRangeAsync(InstructorsData.Create(), token),
                _dbContext.AddRangeAsync(WorkoutTypesData.Create(), token));
            await _dbContext.SaveChangesAsync(token);

            var seededInstructors = await _instructorRepository.GetAllAsync(false, token);
            var seededWorkoutTypes = await _workoutTypeRepository.GetAllAsync(false, token);
            await _dbContext.AddRangeAsync(RealWorkoutsData.Create(seededInstructors, seededWorkoutTypes), token);
            await _dbContext.AddRangeAsync(RepetitiveWorkoutsData.Create(seededInstructors, seededWorkoutTypes), token);
            await _dbContext.SaveChangesAsync(token);

            await _dbContext.WorkoutTypeInstructors.AddRangeAsync
            (
                new WorkoutTypeInstructor(seededInstructors[0].Id, seededWorkoutTypes[0].Id),
                new WorkoutTypeInstructor(seededInstructors[0].Id, seededWorkoutTypes[1].Id),
                new WorkoutTypeInstructor(seededInstructors[1].Id, seededWorkoutTypes[3].Id),
                new WorkoutTypeInstructor(seededInstructors[2].Id, seededWorkoutTypes[2].Id),
                new WorkoutTypeInstructor(seededInstructors[2].Id, seededWorkoutTypes[3].Id)
            );
            await _dbContext.SaveChangesAsync(token);
            _logger.LogInformation("Initial data was successfully seeded");
        }
        catch
        {
            _logger.LogWarning("Initial data seeding failed");
        }
    }
}
