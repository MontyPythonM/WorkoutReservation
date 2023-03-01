namespace WorkoutReservation.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Resource not exist")
    {
    }
    
    public NotFoundException(string message) : base(message)
    {
    }
    
    public NotFoundException(string nameof, string id) : base($"{nameof} with Id: {id} not found")
    {
    }
}
