using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class BiographyLengthLimitExceedException : DomainException
{
    public BiographyLengthLimitExceedException(int characterLimit) 
        : base($"Biography cannot be longer than {characterLimit}")
    {
    }
}