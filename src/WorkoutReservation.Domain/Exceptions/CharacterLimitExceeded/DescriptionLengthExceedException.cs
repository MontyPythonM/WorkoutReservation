using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class DescriptionLengthExceedException : DomainException
{
    public DescriptionLengthExceedException(int characterLimit) 
        : base($"Description cannot be longer than {characterLimit}")
    {
    }
}