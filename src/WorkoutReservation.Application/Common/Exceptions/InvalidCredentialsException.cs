namespace WorkoutReservation.Application.Common.Exceptions;

public class InvalidCredentialsException : Exception
{    
    public InvalidCredentialsException() : base("Invalid email address or password")
    {
    }
    
    public InvalidCredentialsException(string message) : base(message)
    {
    }
}