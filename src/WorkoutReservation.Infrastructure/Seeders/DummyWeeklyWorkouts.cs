using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Seeders
{
    internal class DummyWeeklyWorkouts
    {
        internal static List<RepetitiveWorkout> GetWorkouts()
        {
            var particularWorkouts = new List<RepetitiveWorkout>
            {
                new RepetitiveWorkout()
                {
                    StartTime = new TimeOnly(10, 15),
                    EndTime = new TimeOnly(11, 00),
                    MaxParticipianNumber = 40,
                    DayOfWeek = DayOfWeek.Tuesday,
                    CreatedBy = "Dummy Admin",
                    CreatedDate = new DateTime(2022, 06, 10)                   
                },

                new RepetitiveWorkout()
                {
                    StartTime = new TimeOnly(14, 15),
                    EndTime = new TimeOnly(15, 00),
                    MaxParticipianNumber = 25,
                    DayOfWeek = DayOfWeek.Friday,               
                    CreatedBy = "Dummy Manager",
                    CreatedDate = new DateTime(2022, 06, 13)
                },
            };

            return particularWorkouts;
        }
    }
}
