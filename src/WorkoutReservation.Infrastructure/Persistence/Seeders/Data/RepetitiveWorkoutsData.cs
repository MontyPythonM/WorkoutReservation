using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal sealed class RepetitiveWorkoutsData
{
    internal static IEnumerable<RepetitiveWorkout> Create(List<Instructor> instructors, List<WorkoutType> workoutTypes)
    {
        if (!instructors.Any() || !workoutTypes.Any())
            return new List<RepetitiveWorkout>();

        var repetitiveWorkouts = new List<RepetitiveWorkout>
        {
            new(10, new TimeOnly(8, 00), new TimeOnly(9, 00), DayOfWeek.Monday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(9, 00), new TimeOnly(10, 00), DayOfWeek.Monday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(10, 15), new TimeOnly(12, 00), DayOfWeek.Monday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(13, 00), new TimeOnly(15, 00), DayOfWeek.Monday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(15, 00), new TimeOnly(16, 00), DayOfWeek.Monday, GetRandom(workoutTypes), GetRandom(instructors), true),
            
            new(10, new TimeOnly(7, 00), new TimeOnly(8, 00), DayOfWeek.Tuesday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(8, 00), new TimeOnly(10, 00), DayOfWeek.Tuesday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(10, 30), new TimeOnly(15, 00), DayOfWeek.Tuesday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(15, 00), new TimeOnly(18, 00), DayOfWeek.Tuesday, GetRandom(workoutTypes), GetRandom(instructors), true),

            new(10, new TimeOnly(7, 00), new TimeOnly(8, 00), DayOfWeek.Wednesday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(8, 00), new TimeOnly(9, 00), DayOfWeek.Wednesday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(10, 00), new TimeOnly(13, 00), DayOfWeek.Wednesday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(13, 15), new TimeOnly(15, 00), DayOfWeek.Wednesday, GetRandom(workoutTypes), GetRandom(instructors), true),            
            new(10, new TimeOnly(15, 15), new TimeOnly(17, 00), DayOfWeek.Wednesday, GetRandom(workoutTypes), GetRandom(instructors), true),
            
            new(10, new TimeOnly(7, 00), new TimeOnly(9, 00), DayOfWeek.Thursday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(9, 00), new TimeOnly(10, 00), DayOfWeek.Thursday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(10, 00), new TimeOnly(13, 00), DayOfWeek.Thursday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(13, 00), new TimeOnly(17, 00), DayOfWeek.Thursday, GetRandom(workoutTypes), GetRandom(instructors), true),

            new(10, new TimeOnly(7, 30), new TimeOnly(11, 00), DayOfWeek.Friday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(12, 00), new TimeOnly(14, 00), DayOfWeek.Friday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(14, 00), new TimeOnly(16, 00), DayOfWeek.Friday, GetRandom(workoutTypes), GetRandom(instructors), true),
            new(10, new TimeOnly(16, 00), new TimeOnly(20, 00), DayOfWeek.Friday, GetRandom(workoutTypes), GetRandom(instructors), true),
        };

        return repetitiveWorkouts;
    }

    private static T GetRandom<T>(IReadOnlyList<T> collection)
    {
        var random = new Random();
        var index = random.Next(0, collection.Count);
        return collection[index];
    }
}
