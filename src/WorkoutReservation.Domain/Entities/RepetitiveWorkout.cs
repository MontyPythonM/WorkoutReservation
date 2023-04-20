using WorkoutReservation.Domain.Abstractions;
using WorkoutReservation.Domain.Exceptions;

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
            throw new DomainException(this, nameof(DayOfWeek), ExceptionCode.ValueOutOfRange);
        
        if (MaxParticipantNumber <= 0)
            throw new DomainException(this, nameof(MaxParticipantNumber), ExceptionCode.ValueToSmall);

        if (StartTime > EndTime)
            throw new DomainException(this, nameof(EndTime), ExceptionCode.InvalidValue);
        
        if (WorkoutType is null)
            throw new DomainException(this, nameof(WorkoutType), ExceptionCode.CannotBeNull);
        
        if (Instructor is null)
            throw new DomainException(this, nameof(Instructor), ExceptionCode.CannotBeNull);
    }
}
