using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class ContentTypeOutOfRangeException : DomainException
{
    public ContentTypeOutOfRangeException() : base("ContentType value cannot be out of enum range")
    {
    }
}