using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class DescriptionCannotBeNullOrWhitespaceException : DomainException
{
    public DescriptionCannotBeNullOrWhitespaceException() : base("Description cannot be null or whitespace")
    {
    }
}