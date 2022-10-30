﻿using System.Text.Json.Serialization;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.RealWorkouts.Queries.GetRealWorkoutFromUpcomingWeek;

public class RealWorkoutFromUpcomingWeekDto
{
    public int Id { get; set; }
    public int CurrentParticipantNumber { get; set; }
    public int MaxParticipantNumber { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public WorkoutTypeForRealWorkoutFromUpcomingWeekDto WorkoutType { get; set; }
    public InstructorForRealWorkoutFromUpcomingWeekDto Instructor { get; set; }
}

public class WorkoutTypeForRealWorkoutFromUpcomingWeekDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WorkoutIntensity Intensity { get; set; }
}

public class InstructorForRealWorkoutFromUpcomingWeekDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
