﻿using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromCurrentWeek;

public class RealWorkoutFromCurrentWeekDto
{
    public int Id { get; set; }
    public int CurrentParticipantNumber { get; set; }
    public int MaxParticipantNumber { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public WorkoutTypeForRealWorkoutFromCurrentWeekDto WorkoutType { get; set; }
    public InstructorForRealWorkoutFromCurrentWeekDto Instructor { get; set; }
}

public class WorkoutTypeForRealWorkoutFromCurrentWeekDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WorkoutIntensity Intensity { get; set; }
}

public class InstructorForRealWorkoutFromCurrentWeekDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
