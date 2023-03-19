using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Exceptions;

namespace WorkoutReservation.Domain.Abstractions;

public abstract class BaseWorkout : Entity
{
    public int Id { get; private set; }
    public int MaxParticipantNumber { get; protected set; }
    public TimeOnly StartTime { get; protected set; }
    public TimeOnly EndTime { get; protected set; }
    public WorkoutType WorkoutType { get; protected set; }
    public int WorkoutTypeId { get; internal set; }
    public Instructor Instructor { get; protected set; }
    public int InstructorId { get; internal set; }

    protected BaseWorkout()
    {
        // for EF
    }

    protected BaseWorkout(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime, WorkoutType workoutType, Instructor instructor)
    {
        MaxParticipantNumber = maxParticipantNumber;
        StartTime = startTime;
        EndTime = endTime;
        WorkoutType = workoutType;
        Instructor = instructor;
        Valid();
    }
    
    protected void AddInstructor(Instructor instructor)
    {
        Instructor = instructor;
        Valid();
    }

    protected void AddWorkoutType(WorkoutType workoutType)
    {
        WorkoutType = workoutType;
        Valid();
    }
    
    protected override void Valid()
    {
        if (MaxParticipantNumber <= 0)
            throw new DomainException(this, nameof(MaxParticipantNumber), ExceptionCode.ValueToSmall);

        if (StartTime > EndTime)
            throw new DomainException(this, nameof(EndTime), ExceptionCode.InvalidValue);
        
        if (WorkoutType is null)
            throw new DomainException(this, nameof(WorkoutType), ExceptionCode.CannotBeNull);
        
        if (WorkoutTypeId > 0)
            throw new DomainException(this, nameof(WorkoutTypeId), ExceptionCode.InvalidValue);

        if (Instructor is null)
            throw new DomainException(this, nameof(Instructor), ExceptionCode.CannotBeNull);
        
        if (InstructorId > 0)
            throw new DomainException(this, nameof(InstructorId), ExceptionCode.InvalidValue);
    }
}
