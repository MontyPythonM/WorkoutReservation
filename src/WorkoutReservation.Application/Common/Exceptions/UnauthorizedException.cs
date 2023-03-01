namespace WorkoutReservation.Application.Common.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Access forbidden")
    {
    }
    
    public UnauthorizedException(string message) : base(message)
    {
    }
}