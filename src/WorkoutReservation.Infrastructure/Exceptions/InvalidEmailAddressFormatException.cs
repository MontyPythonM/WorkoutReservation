using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Infrastructure.Exceptions;

public class InvalidEmailAddressFormatException : InfrastructureException
{
    public InvalidEmailAddressFormatException(string email) : base($"Email address: {email} has invalid format")
    {
    }
}