using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class LastNameLengthExceedException : DomainException
{
    public LastNameLengthExceedException(int characterLimit) 
        : base($"Last name cannot be longer than {characterLimit}")
    {
    }
}