using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class EmailCannotBeNullOrWhitespaceException : DomainException
{
    public EmailCannotBeNullOrWhitespaceException() : base("Email cannot be null or whitespace")
    {
    }
}