using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;
using WorkoutReservation.Infrastructure.Seeders.Data;

namespace WorkoutReservation.Infrastructure.Seeders;

public class SeedDummyData
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<SeedDummyData> _logger;

    public SeedDummyData(AppDbContext dbContext, ILogger<SeedDummyData> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Instructors.Any() && !_dbContext.WorkoutTypes.Any() && 
                !_dbContext.RealWorkouts.Any() && !_dbContext.RepetitiveWorkouts.Any())
            {
                _dbContext.AddRange(DummyInstructors.GetInstructors());
                _dbContext.AddRange(DummyWorkoutTypes.GetWorkoutTypes());
                _dbContext.AddRange(DummyRealWorkouts.GetWorkouts());
                _dbContext.AddRange(DummyWeeklyWorkouts.GetWorkouts());
                _dbContext.AddRange(DummyUsers.GetUsers());
                _dbContext.SaveChanges();

                var instructors = _dbContext.Instructors.ToList();
                var workoutTypes = _dbContext.WorkoutTypes.ToList();

                _dbContext.WorkoutTypeInstructors.AddRange
                    (
                        new WorkoutTypeInstructor { InstructorId = instructors[0].Id, WorkoutTypeId = workoutTypes[0].Id },
                        new WorkoutTypeInstructor { InstructorId = instructors[0].Id, WorkoutTypeId = workoutTypes[1].Id },
                        new WorkoutTypeInstructor { InstructorId = instructors[1].Id, WorkoutTypeId = workoutTypes[3].Id },
                        new WorkoutTypeInstructor { InstructorId = instructors[2].Id, WorkoutTypeId = workoutTypes[2].Id },
                        new WorkoutTypeInstructor { InstructorId = instructors[2].Id, WorkoutTypeId = workoutTypes[3].Id }
                    );
                _dbContext.SaveChanges();
                
                _logger.LogInformation("Dummy data was seeded.");                                       
            }
        }
        else 
        {
            throw new InternalServerError("Cannot connect with database.");
        }
    }

}
