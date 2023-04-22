using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Infrastructure.Exceptions;

public class CannotConnectToDatabaseException : InfrastructureException
{
    public CannotConnectToDatabaseException() : base("Application cannot connect with database")
    {
    }
}