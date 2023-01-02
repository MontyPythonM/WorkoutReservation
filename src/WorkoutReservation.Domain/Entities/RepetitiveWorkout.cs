using WorkoutReservation.Domain.Common;

namespace WorkoutReservation.Domain.Entities;

public class RepetitiveWorkout : BaseWorkout
{
    public DayOfWeek DayOfWeek { get; set; }

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
