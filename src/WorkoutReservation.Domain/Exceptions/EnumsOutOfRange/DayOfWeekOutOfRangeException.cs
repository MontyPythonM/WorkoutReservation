using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class DayOfWeekOutOfRangeException : DomainException
{
    public DayOfWeekOutOfRangeException() : base("DayOfWeek value cannot be out of enum range")
    {
    }
}