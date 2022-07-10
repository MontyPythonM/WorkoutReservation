namespace WorkoutReservation.Application.Common.Exceptions;

public class InternalServerError : Exception
{
    public InternalServerError(string message) : base(message)
    {
    }
}
