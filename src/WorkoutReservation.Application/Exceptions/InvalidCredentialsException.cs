using ApplicationException = WorkoutReservation.Shared.Exceptions.ApplicationException;

namespace WorkoutReservation.Application.Exceptions;

public class InvalidCredentialsException : ApplicationException
{    
    public InvalidCredentialsException() 
        : base("Invalid email address or password", System.Net.HttpStatusCode.Conflict)
    {
    }
}