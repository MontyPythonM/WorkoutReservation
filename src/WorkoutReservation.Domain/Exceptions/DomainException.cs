using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException() : base(string.Empty)
    {
    }
    
    public DomainException(string message) : base(message)
    {
    }
    
    public DomainException(object entity, string property, ExceptionCode code) : 
        base($"{entity.ToString()}.{property} - Validation exception: {code.StringValue()}")
    {
    }
}