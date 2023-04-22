using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class WorkoutTypeTagLengthExceed : DomainException
{
    public WorkoutTypeTagLengthExceed(int characterLimit)
        : base($"Tag cannot be longer than {characterLimit}")
    {
    }
}