using System.Net;
using ApplicationException = WorkoutReservation.Shared.Exceptions.ApplicationException;

namespace WorkoutReservation.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException() 
        : base("Resource not exist", System.Net.HttpStatusCode.NotFound)
    {
    }
    
    public NotFoundException(string message) 
        : base(message, System.Net.HttpStatusCode.NotFound)
    {
    }
    
    public NotFoundException(string propertyName, string id) 
        : base($"{propertyName} with Id: {id} not found", System.Net.HttpStatusCode.NotFound)
    {
    }
}
