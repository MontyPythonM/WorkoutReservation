using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class StartTimeGreaterThanEndTimeException : DomainException
{
    public StartTimeGreaterThanEndTimeException(TimeOnly startTime, TimeOnly endTime) 
        : base($"StartTime (${startTime}) cannot be greater than EndTime (${endTime})")
    {
    }
}