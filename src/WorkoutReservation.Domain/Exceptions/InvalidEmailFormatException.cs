using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class InvalidEmailFormatException : DomainException
{
    public InvalidEmailFormatException(string email) : base($"Email address: {email} has invalid format")
    {
    }
}