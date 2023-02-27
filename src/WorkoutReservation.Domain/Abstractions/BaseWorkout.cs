using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Exceptions;

namespace WorkoutReservation.Domain.Abstractions;

public abstract class BaseWorkout
{
    public int Id { get; set; }
    public int MaxParticipantNumber { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public WorkoutType WorkoutType { get; set; }
    public int? WorkoutTypeId { get; set; }
    public Instructor Instructor { get; set; }
    public int? InstructorId { get; set; }

    protected BaseWorkout()
    {
        // for EF
    }
    
    protected BaseWorkout(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime)
    {
        MaxParticipantNumber = maxParticipantNumber;
        StartTime = startTime;
        EndTime = endTime;
        Valid();
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
    
    public void UpdateLastModifiedDate() => LastModifiedDate = DateTime.Now;
    public void UpdateLastModifiedBy(ApplicationUser user) => LastModifiedBy = user.Id.ToString();
    public void SetCreatedDate() => CreatedDate = DateTime.Now;
    public void SetCreatedBy(ApplicationUser user) => CreatedBy = user.Id.ToString();

    private void Valid()
    {
        if (MaxParticipantNumber <= 0)
        {
            throw new DomainException("MaxParticipantNumber must be greater than 0");
        }

        if (StartTime > EndTime)
        {
            throw new DomainException("EndTime must be greater than or equal StartTime");
        }

        if (WorkoutType is null)
        {
            throw new DomainException("WorkoutType cannot be null");
        }
        
        if (Instructor is null)
        {
            throw new DomainException("Instructor cannot be null");
        }
    }
}
