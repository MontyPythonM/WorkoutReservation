namespace WorkoutReservation.Application.Common.Exceptions;

public class DatabaseConnectionException : Exception
{
    public DatabaseConnectionException() : base("Cannot connect with database")
    {
    }
    
    public DatabaseConnectionException(string message) : base(message)
    {
    }
}
