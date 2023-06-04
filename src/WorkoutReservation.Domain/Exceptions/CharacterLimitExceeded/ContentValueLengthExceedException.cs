using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class ContentValueLengthExceedException : DomainException
{
    public ContentValueLengthExceedException(int characterLimit) 
        : base($"Description cannot be longer than {characterLimit}")
    {
    }   
}