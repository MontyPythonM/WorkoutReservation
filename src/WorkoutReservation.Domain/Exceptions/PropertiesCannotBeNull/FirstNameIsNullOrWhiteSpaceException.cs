using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class FirstNameIsNullOrWhiteSpaceException : DomainException
{
    public FirstNameIsNullOrWhiteSpaceException() 
        : base("First name cannot be null or whitespace")
    {
    }
}