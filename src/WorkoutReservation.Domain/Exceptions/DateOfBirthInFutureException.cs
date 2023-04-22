using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class DateOfBirthInFutureException : DomainException
{
    public DateOfBirthInFutureException() : base("date of birth cannot be in the future")
    {
    }
}