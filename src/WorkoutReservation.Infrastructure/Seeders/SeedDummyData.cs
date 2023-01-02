using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;
using WorkoutReservation.Infrastructure.Seeders.Data;

namespace WorkoutReservation.Infrastructure.Seeders;

public class SeedDummyData
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<SeedDummyData> _logger;
    private readonly IInstructorRepository _instructorRepository;
    private readonly IWorkoutTypeRepository _workoutTypeRepository;

    public SeedDummyData(AppDbContext dbContext, 
        ILogger<SeedDummyData> logger, 
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
            throw new InternalServerError("Cannot connect with database.");
        
        if (await _dbContext.Instructors.AnyAsync(token) ||
            await _dbContext.WorkoutTypes.AnyAsync(token) ||
            await _dbContext.RealWorkouts.AnyAsync(token) ||
            await _dbContext.RepetitiveWorkouts.AnyAsync(token))
        {        
            return;
        }

        await Task.WhenAll(
            _dbContext.AddRangeAsync(DummyInstructors.GetInstructors(), token),
            _dbContext.AddRangeAsync(DummyWorkoutTypes.GetWorkoutTypes(), token));
        await _dbContext.SaveChangesAsync(token);

        var seededInstructors = await _instructorRepository.GetAllAsync(true, token);
        var seededWorkoutTypes = await _workoutTypeRepository.GetAllAsync(true, token);

        await Task.WhenAll(
            _dbContext.AddRangeAsync(DummyRealWorkouts.GetRealWorkouts(), token),
            _dbContext.AddRangeAsync(DummyWeeklyWorkouts.GetRepetitiveWorkouts(seededInstructors, seededWorkoutTypes), token),
            _dbContext.AddRangeAsync(DummyUsers.GetUsers(), token));
        await _dbContext.SaveChangesAsync(token);
        
        await _dbContext.WorkoutTypeInstructors.AddRangeAsync
        (
            new WorkoutTypeInstructor { InstructorId = seededInstructors[0].Id, WorkoutTypeId = seededWorkoutTypes[0].Id },
            new WorkoutTypeInstructor { InstructorId = seededInstructors[0].Id, WorkoutTypeId = seededWorkoutTypes[1].Id },
            new WorkoutTypeInstructor { InstructorId = seededInstructors[1].Id, WorkoutTypeId = seededWorkoutTypes[3].Id },
            new WorkoutTypeInstructor { InstructorId = seededInstructors[2].Id, WorkoutTypeId = seededWorkoutTypes[2].Id },
            new WorkoutTypeInstructor { InstructorId = seededInstructors[2].Id, WorkoutTypeId = seededWorkoutTypes[3].Id }
        );
        await _dbContext.SaveChangesAsync(token);
        _logger.LogInformation("Dummy data was successfully seeded.");
    }
}
