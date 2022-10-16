using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

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
                MaxParticipiantNumber = 40,
                DayOfWeek = DayOfWeek.Sunday,
                CreatedBy = "Dummy Admin",
                CreatedDate = new DateTime(2022, 06, 10),
                InstructorId = 2,
                WorkoutTypeId = 3
            },

            new RepetitiveWorkout()
            {
                StartTime = new TimeOnly(14, 15),
                EndTime = new TimeOnly(15, 00),
                MaxParticipiantNumber = 25,
                DayOfWeek = DayOfWeek.Sunday,
                CreatedBy = "Dummy Manager",
                CreatedDate = new DateTime(2022, 06, 13),
                InstructorId = 1,
                WorkoutTypeId = 2
            },
        };

        return particularWorkouts;
    }
}
