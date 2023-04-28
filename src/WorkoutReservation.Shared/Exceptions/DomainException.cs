using WorkoutReservation.Shared.Extensions;

namespace WorkoutReservation.Shared.Exceptions;

public class DomainException : Exception
{
    public DomainException() : base(string.Empty)
    {
    }
    
    public DomainException(string message) : base(message)
    {
    }
}