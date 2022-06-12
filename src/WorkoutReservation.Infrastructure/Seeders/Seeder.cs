using Microsoft.Extensions.Logging;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Seeders
{
    public class Seeder
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<Seeder> _logger;

        public Seeder(AppDbContext dbContext, ILogger<Seeder> logger)
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
                    _dbContext.SaveChanges();

                    var i1 = _dbContext.Instructors.First();
                    var i2 = _dbContext.Instructors.Skip(1).First();
                    var i3 = _dbContext.Instructors.Skip(2).First();
                    var wt1 = _dbContext.WorkoutTypes.First();
                    var wt2 = _dbContext.WorkoutTypes.Skip(1).First();
                    var wt3 = _dbContext.WorkoutTypes.Skip(2).First();
                    var wt4 = _dbContext.WorkoutTypes.Skip(3).First();

                    _dbContext.WorkoutTypeInstructors.AddRange
                        (
                            new WorkoutTypeInstructor { InstructorId = i1.Id, WorkoutTypeId = wt1.Id },
                            new WorkoutTypeInstructor { InstructorId = i1.Id, WorkoutTypeId = wt2.Id },
                            new WorkoutTypeInstructor { InstructorId = i2.Id, WorkoutTypeId = wt4.Id },
                            new WorkoutTypeInstructor { InstructorId = i3.Id, WorkoutTypeId = wt3.Id },
                            new WorkoutTypeInstructor { InstructorId = i3.Id, WorkoutTypeId = wt4.Id }
                        );
                    _dbContext.SaveChanges();
                    
                    var ww1 = _dbContext.RepetitiveWorkouts.First();
                    var ww2 = _dbContext.RepetitiveWorkouts.Skip(1).First();
                    var pw1 = _dbContext.RealWorkouts.First();
                    var pw2 = _dbContext.RealWorkouts.Skip(1).First();
                    
                    ww1.InstructorId = i1.Id;
                    ww1.WorkoutTypeId = wt1.Id;

                    ww2.InstructorId = i2.Id;
                    ww2.WorkoutTypeId = wt2.Id; 

                    pw1.InstructorId = i3.Id;
                    pw1.WorkoutTypeId = wt3.Id;

                    pw2.InstructorId = i1.Id;
                    pw2.WorkoutTypeId = wt4.Id;
                    
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
}
