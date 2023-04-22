using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class NoteLengthLimitExceedException : DomainException
{
    public NoteLengthLimitExceedException(int characterLimit)
        : base($"Note cannot be longer than {characterLimit}")
    {
    }
}