using WorkoutReservation.Domain.Abstractions;
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
    
    public DomainException(object property, string message) : 
        base($"{property.ToString()} {message}")
    {
    }
    
    public DomainException(Entity entity, object property, ExceptionCode code) : 
        base($"{entity.ToString()}.{property.ToString()} - {code.StringValue()}")
    {
    }
}