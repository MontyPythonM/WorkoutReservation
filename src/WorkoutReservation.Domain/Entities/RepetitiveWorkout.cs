using WorkoutReservation.Domain.Abstractions;

namespace WorkoutReservation.Domain.Entities;

public class RepetitiveWorkout : BaseWorkout
{
    public DayOfWeek DayOfWeek { get; set; }

    protected RepetitiveWorkout()
    {
    }
    
    public RepetitiveWorkout(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime, DayOfWeek dayOfWeek) 
        : base(maxParticipantNumber, startTime, endTime)
    {
        DayOfWeek = dayOfWeek;
    }
    
    public RepetitiveWorkout(int maxParticipantNumber, TimeOnly startTime, TimeOnly endTime, DayOfWeek dayOfWeek, WorkoutType workoutType, Instructor instructor) 
        : base(maxParticipantNumber, startTime, endTime, workoutType, instructor)
    {
        DayOfWeek = dayOfWeek;
    }
}
