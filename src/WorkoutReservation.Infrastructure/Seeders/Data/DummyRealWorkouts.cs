﻿using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal class DummyRealWorkouts
{
    internal static List<RealWorkout> GetWorkouts()
    {
        var particularWorkouts = new List<RealWorkout>
        {
            new RealWorkout()
            {
                StartTime = new TimeOnly(10, 15),
                EndTime = new TimeOnly(11, 00),
                Date = new DateOnly(2022, 06, 14),
                MaxParticipiantNumber = 10,
                CurrentParticipiantNumber = 4,
                CreatedBy = "Dummy Admin",
                CreatedDate = DateTime.Now,
                IsAutoGenerated = false
            },

            new RealWorkout()
            {
                StartTime = new TimeOnly(20, 15),
                EndTime = new TimeOnly(21, 00),
                Date = new DateOnly(2022, 06, 21),
                MaxParticipiantNumber = 15,
                CurrentParticipiantNumber = 8,
                CreatedDate = DateTime.Now,
                IsAutoGenerated = true
            },
        };

        return particularWorkouts;
    }

}
