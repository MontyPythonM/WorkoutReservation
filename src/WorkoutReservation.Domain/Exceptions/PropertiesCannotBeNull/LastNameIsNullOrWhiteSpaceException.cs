using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class LastNameIsNullOrWhiteSpaceException : DomainException
{
    public LastNameIsNullOrWhiteSpaceException() : base("Last name cannot be null or whitespace")
    {
    }
}