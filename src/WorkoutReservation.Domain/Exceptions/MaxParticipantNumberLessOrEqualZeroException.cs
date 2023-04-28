using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class MaxParticipantNumberLessOrEqualZeroException : DomainException
{
    public MaxParticipantNumberLessOrEqualZeroException() 
        : base("Max participant number cannot be less or equal zero")
    {
    }
}