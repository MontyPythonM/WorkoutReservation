﻿using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Methods;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal sealed class DummyRealWorkouts
{
    internal static IEnumerable<RealWorkout> GetRealWorkouts()
    {
        var currentDate = DateTime.Now.GetFirstDayOfWeek();
        
        var particularWorkouts = new List<RealWorkout>
        {
            new()
            {
                StartTime = new TimeOnly(10, 00),
                EndTime = new TimeOnly(11, 00),
                Date = currentDate.AddDays(1),
                MaxParticipantNumber = 10,
                CurrentParticipantNumber = 4,
                CreatedBy = "Dummy Admin",
                CreatedDate = DateTime.Now,
                IsAutoGenerated = false,
                InstructorId = 1,
                WorkoutTypeId = 1
            },
            new()
            {
                StartTime = new TimeOnly(11, 00),
                EndTime = new TimeOnly(14, 00),
                Date = currentDate.AddDays(1),
                MaxParticipantNumber = 10,
                CurrentParticipantNumber = 8,
                CreatedDate = DateTime.Now,
                IsAutoGenerated = true,
                InstructorId = 2,
                WorkoutTypeId = 2
            },
            new()
            {
                StartTime = new TimeOnly(14, 30),
                EndTime = new TimeOnly(17, 00),
                Date = currentDate.AddDays(1),
                MaxParticipantNumber = 10,
                CurrentParticipantNumber = 0,
                CreatedDate = DateTime.Now,
                IsAutoGenerated = true,
                InstructorId = 2,
                WorkoutTypeId = 1
            },
            new ()
            {
                StartTime = new TimeOnly(8, 15),
                EndTime = new TimeOnly(10, 00),
                Date = currentDate.AddDays(4),
                MaxParticipantNumber = 15,
                CurrentParticipantNumber = 0,
                CreatedDate = DateTime.Now,
                IsAutoGenerated = true,
                InstructorId = 3,
                WorkoutTypeId = 2
            },
            new ()
            {
                StartTime = new TimeOnly(10, 15),
                EndTime = new TimeOnly(12, 00),
                Date = currentDate.AddDays(4),
                MaxParticipantNumber = 22,
                CurrentParticipantNumber = 0,
                CreatedDate = DateTime.Now,
                IsAutoGenerated = true,
                InstructorId = 3,
                WorkoutTypeId = 3
            },
        };
        return particularWorkouts;
    }
}
