using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal sealed class RealWorkoutsData
{
    public static IEnumerable<RealWorkout> Create(List<Instructor> instructors, List<WorkoutType> workoutTypes)
    {
        if (!instructors.Any() || !workoutTypes.Any())
            return new List<RealWorkout>();
        
        var currentDate = DateTime.Now.GetFirstDayOfWeek();
        var realWorkouts = new List<RealWorkout>
        {
            new(1, new TimeOnly(8, 00), new TimeOnly(11, 00), GetRandom(workoutTypes), GetRandom(instructors), currentDate, true),
            new(5, new TimeOnly(11, 15), new TimeOnly(13, 00), GetRandom(workoutTypes), GetRandom(instructors), currentDate, true),
            new(10, new TimeOnly(15, 00), new TimeOnly(17, 00), GetRandom(workoutTypes), GetRandom(instructors), currentDate, true),
            new(15, new TimeOnly(10, 00), new TimeOnly(11, 00), GetRandom(workoutTypes), GetRandom(instructors), currentDate.AddDays(2), true),
            new(100, new TimeOnly(11, 00), new TimeOnly(13, 00), GetRandom(workoutTypes), GetRandom(instructors), currentDate.AddDays(2), true),
            new(100, new TimeOnly(13, 30), new TimeOnly(17, 00), GetRandom(workoutTypes), GetRandom(instructors), currentDate.AddDays(2), true),
            new(20, new TimeOnly(7, 00), new TimeOnly(10, 30), GetRandom(workoutTypes), GetRandom(instructors), currentDate.AddDays(4), true),
            new(5, new TimeOnly(10, 00), new TimeOnly(18, 00), GetRandom(workoutTypes), GetRandom(instructors), currentDate.AddDays(5), true),
        };
        
        return realWorkouts;
    }
    
    private static T GetRandom<T>(IReadOnlyList<T> collection)
    {
        var random = new Random();
        var index = random.Next(0, collection.Count);
        return collection[index];
    }
}
