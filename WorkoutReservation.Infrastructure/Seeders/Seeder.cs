using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Infrastructure.Presistence;

namespace WorkoutReservation.Infrastructure.Seeders
{
    public class Seeder
    {
        private readonly AppDbContext _dbContext;

        public Seeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Instructors.Any() && !_dbContext.WorkoutTypes.Any())
                {
                    _dbContext.AddRange(DummyInstructors.GetInstructors());
                    _dbContext.AddRange(DummyWorkoutTypes.GetWorkoutTypes());
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

                    _dbContext.SaveChangesAsync();
                }


            }
        }

    }
}
