using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class ContentValueCannotBeNullException : DomainException
{
    public ContentValueCannotBeNullException() : base("Content value cannot be null or whitespace")
    {
    }
}