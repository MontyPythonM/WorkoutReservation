using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class WorkoutTypeNameLengthExceedException : DomainException
{
    public WorkoutTypeNameLengthExceedException(int characterLimit)
        : base($"WorkoutType name cannot be longer than {characterLimit}")
    {
    }
}