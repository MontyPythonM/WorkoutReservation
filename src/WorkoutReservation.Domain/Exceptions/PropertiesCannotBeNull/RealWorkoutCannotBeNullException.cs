using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class RealWorkoutCannotBeNullException : DomainException
{
    public RealWorkoutCannotBeNullException() : base("Real workout cannot be null")
    {
    }
}