using WorkoutReservation.Domain.Entities.Workout;

namespace WorkoutReservation.Infrastructure.Seeders
{
    internal class DummyWeeklyWorkouts
    {
        internal static List<WeeklyWorkout> GetWorkouts()
        {
            var particularWorkouts = new List<WeeklyWorkout>
            {
                new WeeklyWorkout()
                {
                    StartTime = new TimeOnly(10, 15),
                    EndTime = new TimeOnly(11, 00),
                    MaxParticipianNumber = 40,
                    DayOfWeek = DayOfWeek.Monday,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "DummyAdmin2",                                   
                },

                new WeeklyWorkout()
                {
                    StartTime = new TimeOnly(14, 15),
                    EndTime = new TimeOnly(15, 00),
                    MaxParticipianNumber = 25,
                    DayOfWeek = DayOfWeek.Friday,
                    CreatedDate = new DateTime(2022, 06, 10),
                    CreatedBy = "DummyAdmin3",
                },
            };

            return particularWorkouts;
        }
    }
}
