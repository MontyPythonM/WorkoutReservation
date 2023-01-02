﻿using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal sealed class DummyWeeklyWorkouts
{
    internal static IEnumerable<RepetitiveWorkout> GetRepetitiveWorkouts(List<Instructor> seededInstructors, List<WorkoutType> seededWorkoutTypes)
    {
        if (!seededInstructors.Any() || !seededWorkoutTypes.Any())
            return new List<RepetitiveWorkout>();

        var timetableWorkouts = new List<RepetitiveWorkout>
        {
            new(10, new TimeOnly(8, 00), new TimeOnly(9, 00), DayOfWeek.Monday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(9, 00), new TimeOnly(10, 00), DayOfWeek.Monday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(10, 15), new TimeOnly(12, 00), DayOfWeek.Monday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(13, 00), new TimeOnly(15, 00), DayOfWeek.Monday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(15, 00), new TimeOnly(16, 00), DayOfWeek.Monday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            
            new(10, new TimeOnly(7, 00), new TimeOnly(8, 00), DayOfWeek.Tuesday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(8, 00), new TimeOnly(10, 00), DayOfWeek.Tuesday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(10, 30), new TimeOnly(15, 00), DayOfWeek.Tuesday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(15, 00), new TimeOnly(18, 00), DayOfWeek.Tuesday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),

            new(10, new TimeOnly(7, 00), new TimeOnly(8, 00), DayOfWeek.Wednesday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(8, 00), new TimeOnly(9, 00), DayOfWeek.Wednesday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(10, 00), new TimeOnly(13, 00), DayOfWeek.Wednesday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(13, 15), new TimeOnly(15, 00), DayOfWeek.Wednesday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),            
            new(10, new TimeOnly(15, 15), new TimeOnly(17, 00), DayOfWeek.Wednesday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            
            new(10, new TimeOnly(7, 00), new TimeOnly(9, 00), DayOfWeek.Thursday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(9, 00), new TimeOnly(10, 00), DayOfWeek.Thursday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(10, 00), new TimeOnly(13, 00), DayOfWeek.Thursday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(13, 00), new TimeOnly(17, 00), DayOfWeek.Thursday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),

            new(10, new TimeOnly(7, 30), new TimeOnly(11, 00), DayOfWeek.Friday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(12, 00), new TimeOnly(14, 00), DayOfWeek.Friday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(14, 00), new TimeOnly(16, 00), DayOfWeek.Friday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
            new(10, new TimeOnly(16, 00), new TimeOnly(20, 00), DayOfWeek.Friday, GetRandomWorkoutType(seededWorkoutTypes), GetRandomInstructor(seededInstructors)),
        };

        return timetableWorkouts;
    }

    private static Instructor GetRandomInstructor(List<Instructor> instructors)
    {
        var random = new Random();
        var index = random.Next(0, instructors.Count);
        return instructors[index];
    }
    
    private static WorkoutType GetRandomWorkoutType(List<WorkoutType> workoutTypes)
    {
        var random = new Random();
        var index = random.Next(0, workoutTypes.Count);
        return workoutTypes[index];
    }
}
