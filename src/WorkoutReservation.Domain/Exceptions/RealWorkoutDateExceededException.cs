using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class RealWorkoutDateExceededException : DomainException
{
    public RealWorkoutDateExceededException()
        : base("The date cannot be further than 2 weeks")
    {
    }
}