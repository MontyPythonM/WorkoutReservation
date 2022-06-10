using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Infrastructure.Seeders
{
    internal class DummyParticularWorkouts
    {
        internal static List<ParticularWorkout> GetWorkouts()
        {
            var particularWorkouts = new List<ParticularWorkout>
            {
                new ParticularWorkout()
                {
                    StartTime = new TimeOnly(10, 15),
                    EndTime = new TimeOnly(11, 00),
                    Date = new DateOnly(2022, 01, 01),
                    MaxParticipianNumber = 10,
                    CurrentParticipianNumber = 1,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "DummyAdmin"
                },

                new ParticularWorkout()
                {
                    StartTime = new TimeOnly(20, 15),
                    EndTime = new TimeOnly(21, 00),
                    Date = new DateOnly(2022, 05, 05),
                    MaxParticipianNumber = 15,
                    CurrentParticipianNumber = 5,                    
                    CreatedDate = new DateTime(2000, 01, 01),
                    CreatedBy = "DummyAdmin"
                },
            };

            return particularWorkouts;
        }

    }
}
