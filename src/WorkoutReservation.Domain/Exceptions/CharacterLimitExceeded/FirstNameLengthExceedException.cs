using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class FirstNameLengthExceedException : DomainException
{
    public FirstNameLengthExceedException(int characterLimit) 
        : base($"First name cannot be longer than {characterLimit}")
    {
    }
}