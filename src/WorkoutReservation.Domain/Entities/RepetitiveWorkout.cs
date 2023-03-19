using WorkoutReservation.Domain.Abstractions;

namespace WorkoutReservation.Domain.Entities;

public sealed class RepetitiveWorkout : BaseWorkout
{
    public DayOfWeek DayOfWeek { get; private set; }
    
    private RepetitiveWorkout()
    {
        // required by EF core
    }
    
    public RepetitiveWorkout(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime, DayOfWeek dayOfWeek, WorkoutType workoutType, Instructor instructor) 
        : base(maxParticipantNumber, startTime, endTime, workoutType, instructor)
    {
        DayOfWeek = dayOfWeek;
        Valid();
    }

    public void Update(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime, 
        DayOfWeek dayOfWeek, WorkoutType workoutType, Instructor instructor)
    {
        MaxParticipantNumber = maxParticipantNumber;
        StartTime = startTime;
        EndTime = endTime;
        DayOfWeek = dayOfWeek;
        AddWorkoutType(workoutType);
        Valid();
    }
    
    protected override void Valid()
    {
        // TODO add validation for enum 
    }
}
