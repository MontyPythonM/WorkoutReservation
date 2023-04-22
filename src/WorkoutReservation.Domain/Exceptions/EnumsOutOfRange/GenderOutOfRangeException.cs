using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class GenderOutOfRangeException : DomainException
{
    public GenderOutOfRangeException() : base("Gender value cannot be out of enum range")
    {
    }
}