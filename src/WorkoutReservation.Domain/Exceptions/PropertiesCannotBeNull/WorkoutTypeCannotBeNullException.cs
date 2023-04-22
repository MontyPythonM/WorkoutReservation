using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class WorkoutTypeCannotBeNullException : DomainException
{
    public WorkoutTypeCannotBeNullException() 
        : base("Workout type cannot be null")
    {
    }
}