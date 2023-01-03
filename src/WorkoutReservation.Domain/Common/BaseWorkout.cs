using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Domain.Common;

public abstract class BaseWorkout : BaseAuditableEntity
{
    public int Id { get; set; }
    public int MaxParticipantNumber { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public WorkoutType WorkoutType { get; set; }
    public int? WorkoutTypeId { get; set; }

    public Instructor Instructor { get; set; }
    public int? InstructorId { get; set; }

    protected BaseWorkout()
    {
    }
    
    protected BaseWorkout(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime)
    {
        MaxParticipantNumber = maxParticipantNumber;
        StartTime = startTime;
        EndTime = endTime;
        
    }
    
    protected BaseWorkout(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime, WorkoutType workoutType, Instructor instructor)
    {
        MaxParticipantNumber = maxParticipantNumber;
        StartTime = startTime;
        EndTime = endTime;
        WorkoutType = workoutType;
        Instructor = instructor;
    }
}
