﻿using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Exceptions;
using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Entities;

public sealed class RepetitiveWorkout : Entity
{
    public int Id { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public int MaxParticipantNumber { get; private set; }
    public WorkoutType WorkoutType { get; private set; }
    public Instructor Instructor { get; private set; }
    
    private RepetitiveWorkout()
    {
        // required by EF core
    }
    
    public RepetitiveWorkout(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime, 
        DayOfWeek dayOfWeek, WorkoutType workoutType, Instructor instructor)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        MaxParticipantNumber = maxParticipantNumber;
        WorkoutType = workoutType;
        Instructor = instructor;
        Valid();
    }

    public void Update(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime, 
        DayOfWeek dayOfWeek, WorkoutType workoutType, Instructor instructor)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        MaxParticipantNumber = maxParticipantNumber;
        WorkoutType = workoutType;
        Instructor = instructor;
        Valid();
    }
    
    protected override void Valid()
    {
        if (!Enum.IsDefined(DayOfWeek))
            throw new DayOfWeekOutOfRangeException();

        if (MaxParticipantNumber <= 0)
            throw new MaxParticipantNumberLessOrEqualZeroException();

        if (StartTime > EndTime)
            throw new StartTimeGreaterThanEndTimeException(StartTime, EndTime);

        if (WorkoutType is null)
            throw new WorkoutTypeCannotBeNullException();

        if (Instructor is null)
            throw new InstructorCannotBeNullException();
    }
}
